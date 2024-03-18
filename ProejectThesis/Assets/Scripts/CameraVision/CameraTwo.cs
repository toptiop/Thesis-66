using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyFieldOfView))]
public class CameraTwo : MonoBehaviour
{
    [Header("Setting vision")]
    public float visionRange = 10;
    public float visionAngle = 360f;

    [Header("Time Active")]
    public float timeActive = 5;
    public bool active = true;
    [SerializeField] private float timer;

    [Header("Lighting")]
    public Light light;

    private EnemyFieldOfView vision;
    [SerializeField]private VisionCone cone;

    private void Awake()
    {
        vision = GetComponent<EnemyFieldOfView>();
   
    }
    private void Start()
    {
        timer = timeActive;
    }
    // Update is called once per frame
    void Update()
    {
       

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            active = !active; // Toggle the active state
            vision.enabled = active;
            vision.visionActive = active;
            light.enabled = active;
            cone.gameObject.SetActive(active);
            timer = timeActive;
        }

       
    }
}