using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class switchcharacters : MonoBehaviour
{
    [Header("[Player Component]")]
    public PlayerController controller;
    public PlayerInput player;
    public Detection playerDetec;
    public CinemachineVirtualCamera camPlayer;
    public StateCommand stateCommand;

    [Header("[Robot Component]")]
    public RobotController robotController;
    public PlayerInput robot;
    public RobotDetection robotDetec;
    public CinemachineVirtualCamera camRobot;
    public AutoPilotRobot aiFollow;


    [Header("Input")]
    public InputManager playerInput;
    public InputRobotManager robotInput;

    [Space(20)]
    public bool isSwitch;
    public bool activeSwitch;
    public bool canSwitch;
    void Start()
    {
        StartCoroutine(DelayeStart());
    }
    IEnumerator DelayeStart()
    {
        yield return new WaitForSeconds(1);
        SwitchControl();
    }
    void FixedUpdate()
    {

        if (!activeSwitch)
        {
            playerInput.swap = false;
            robotInput.swap = false;
            aiFollow.enabled = false;
        }

        if(activeSwitch && isSwitch)
        {
            aiFollow.enabled = true;
            stateCommand.enabled = true;
        }
        else
        {
            aiFollow.enabled = false;
            stateCommand.enabled = false;
        }

        if (activeSwitch )
        {
            if(canSwitch)
            {
                if (playerInput.swap || robotInput.swap)
                {
                    ChangeCharactor();
                    SwitchControl();
                    playerInput.swap = false;
                    robotInput.swap = false;
                }
            }
            else
            {
                playerInput.swap = false;
                robotInput.swap = false;
            }
            
        }



    }


    #region SwitchCharacter
    void SwitchControl()
    {
        if (isSwitch)
        {
            robotDetec.showIcon = false;
            Invoke("DisableControlRobot", 0.1f);
            Invoke("EnableControlPlayer", 0.2f);
        }
        else
        {
            playerDetec.showIcon = false;
            Invoke("DisableControlPlayer", 0.1f);
            Invoke("EnableControlRobot", 0.2f);
        }
    }


    void EnableControlPlayer()
    {
        playerDetec.showIcon = true;
        controller.enabled = true;
        player.enabled = true;
        playerDetec.enabled = true;
        camPlayer.gameObject.SetActive(true);
        stateCommand.enabled = true;
    }

    void DisableControlPlayer()
    {
        ResetAnimationPlayer();

        controller.enabled = false;
        player.enabled = false;
        playerDetec.enabled = false;
        camPlayer.gameObject.SetActive(false);
        stateCommand.enabled = false;
    }

    void EnableControlRobot()
    {
        robotDetec.showIcon = true;
        robotController.enabled = true;
        robot.enabled = true;
        robotDetec.enabled = true;
        camRobot.gameObject.SetActive(true);
        //
        aiFollow.enabled = false;
        aiFollow.agent.enabled = false;
    }
    void DisableControlRobot()
    {
        robotController.enabled = false;
        robot.enabled = false;
        robotDetec.enabled = false;
        camRobot.gameObject.SetActive(false);
        //
        aiFollow.enabled = true;
        aiFollow.agent.enabled = true;
    }
    #endregion

    void ChangeCharactor()
    {
        isSwitch = !isSwitch;
    }


    void ResetAnimationPlayer()
    {
        if (controller._input.move == Vector2.zero)
        {
            controller._animator.SetFloat("Speed", 0);
        }
    }
}
