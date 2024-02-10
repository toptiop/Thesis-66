using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class switchcharacters : MonoBehaviour
{
    [Header("[Player Component]")]
    public PlayerInput player;
    public Detection playerDetec;
    public CinemachineVirtualCamera camPlayer;

    [Header("[Robot Component]")]
    public PlayerInput robot;
    public RobotDetection robotDetec;
    public CinemachineVirtualCamera camRobot;


    [Header("Input")]
    public InputManager playerInput;
    public InputRobotManager robotInput;

    [Space(20)]
    public bool isSwitch;
    public bool activeSwitch;
    void Start()
    {
        Invoke("SwitchControl",1.0f);
    }

    void Update()
    {

        if (!activeSwitch)
        {
            playerInput.swap = false;
            robotInput.swap = false;
        }
        if (activeSwitch)
        {
            if (playerInput.swap || robotInput.swap)
            {
                ChangeCharactor();
                playerInput.swap = false;
                robotInput.swap = false;
            }
        }
        SwitchControl();
    }
    #region SwitchCharacter
    void SwitchControl()
    {
        if (isSwitch)
        {
            DisableControlRobot();
            EnableControlPlayer();            
        }
        else
        {
            DisableControlPlayer();
            EnableControlRobot();
        }
    }

  
    void EnableControlPlayer()
    {
        player.enabled = true;
        playerDetec.enabled = true;
        camPlayer.gameObject.SetActive(true);
    }

    void DisableControlPlayer()
    {
        player.enabled = false;
        playerDetec.enabled = false;
        camPlayer.gameObject.SetActive(false);
    }

    void EnableControlRobot()
    {
        robot.enabled = true;
        robotDetec.enabled = true;
        camRobot.gameObject.SetActive(true);
    }
    void DisableControlRobot()
    {
        robot.enabled = false;
        robotDetec.enabled = false;
        camRobot.gameObject.SetActive(false);
    }
    #endregion

    void ChangeCharactor()
    {
        isSwitch = !isSwitch;
    }

}
