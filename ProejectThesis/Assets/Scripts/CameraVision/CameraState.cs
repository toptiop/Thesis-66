using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraState : MonoBehaviour
{
    public enum State { Search, LockOn};
    public State currentState;

    #region rotation;
    [Header("Setting Rotation")]
    public bool useRotation;
    public float minRotation;
    public float maxRotation;
    public GameObject neck;
    [SerializeField]
    private float currentRotation = 0f;
    public float rotationSpeed = 5f;
    #endregion

    [SerializeField] float timer;
    [SerializeField] float timeDel = 1f;
    public float timeAlert = 5.0f;
    public float resetAlert = 3.0f;

    [SerializeField]
    private EnemyFieldOfView view;

    private void Update()
    {
         UpdateState();
        //LookPlayer();
    }
    void EnterState(State state)
    {
        ExitState();
        currentState = state;

        switch (currentState)
        {
            case State.Search:
                break;
            case State.LockOn:
                timer = resetAlert;
                break;
        }
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case State.Search:
                RotateNeck();
                if (view.canSeePlayer)
                {
                    timer += Time.deltaTime;
                    if (timer >= timeAlert)
                    {
                        EnterState(State.LockOn);                        
                    }
                }
                else
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0f)
                        timer = 0f;
                }
                break;
            case State.LockOn:
                if (!view.canSeePlayer)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0f)
                    {
                        EnterState(State.Search);
                        timer = 0f;
                    }
                }
                else
                {
                    LookPlayer();
                }
                break;
        }
    }


    void ExitState()
    {
        switch (currentState)
        {
            case State.Search:
                break;
            case State.LockOn:
                break;
        }
    }

    void RotateNeck()
    {
        if (useRotation && neck != null)
        {
            currentRotation += rotationSpeed * Time.deltaTime;

            // ตรวจสอบว่ามีการเกิน maxRotation
            if (currentRotation > maxRotation)
            {
                currentRotation = maxRotation;
                rotationSpeed = -rotationSpeed; // เปลี่ยนทิศเพื่อหมุนย้อนกลับ
            }
            // ตรวจสอบว่ามีการต่ำกว่า minRotation
            else if (currentRotation < minRotation)
            {
                currentRotation = minRotation;
                rotationSpeed = -rotationSpeed; // เปลี่ยนทิศเพื่อหมุนย้อนกลับ
            }

            // หมุนออบเจคตามแกน Y
            neck.transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);
        }
    }

    void LookPlayer()
    {
        Debug.Log("Rotation");
        Vector3 targetDirection = view.playerRef.transform.position - neck.transform.position;
        targetDirection.y = 0f; // ไม่ต้องสนใจแกน Y ในการหมุน

        // หาค่าการหมุนของคอที่ต้องหันไปที่ตำแหน่งของผู้เล่น
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // ทำการหมุนคอ
        neck.transform.rotation = Quaternion.Slerp(neck.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
