using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Active ActionUI")]
    public bool InteractUI;
    public bool cameraMove;

    public bool pauseGame;
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
       
        TogglePause(false);
    }

    private void Update()
    {
        ControlActionUI();
        ChangeCursorState(!InteractUI);
 
    }

    public void ControlActionUI()
    {
       if(Singleton.Instance != null )
        {
            if(Singleton.Instance.actionUI != null)
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

    public void Inter(bool newin)
    {
        InteractUI = newin;
    }

    public void ChangeStateInteractUI(bool newState)
    {
        InteractUI = newState;
        Debug.Log("ChangStateMouse");
    }

    public void TogglePause(bool newState)
    {
        if(newState)
        {
            Time.timeScale = 0;
            ChangeStateInteractUI(true);
            Debug.Log("Stop");
        }
        else
        {
            Time.timeScale = 1;
            ChangeStateInteractUI(false);
            cameraMove = false;
            
        }
    }
}
