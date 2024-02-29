using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Active ActionUI")]
    public bool InteractUI;
    public bool cameraMove;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        ChangeCursorState(true);
    }

    private void Update()
    {
        ControlActionUI();
        ChangeCursorState(!InteractUI);
    }

    public void ControlActionUI()
    {
       if(Singleton.Instance != null)
        {
            if (!InteractUI)
            {
                Singleton.Instance.actionUI.gameObject.SetActive(true);
            }
            else
            {
                Singleton.Instance.actionUI.gameObject.SetActive(false);
            }
        }
    }

    public void ChangeCursorState(bool newState)
    {       
        if(newState)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cameraMove = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cameraMove = true;
        }
    }

    public void ChangeStateInteractUI(bool newState)
    {
        InteractUI = newState;
    }
}
