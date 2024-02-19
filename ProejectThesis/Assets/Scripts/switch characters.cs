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
    void Start()
    {
        StartCoroutine(DelayeStart());
    }
    IEnumerator DelayeStart()
    {
        yield return new WaitForSeconds(1);
        SwitchControl();
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
        controller.enabled = true;
        player.enabled = true;
        playerDetec.enabled = true;
        camPlayer.gameObject.SetActive(true);
    }

    void DisableControlPlayer()
    {
        ResetAnimationPlayer();
        controller.enabled = false;
        player.enabled = false;
        playerDetec.enabled = false;
        camPlayer.gameObject.SetActive(false);
    }

    void EnableControlRobot()
    {
        robotController.enabled = true;
        robot.enabled = true;
        robotDetec.enabled = true;
        camRobot.gameObject.SetActive(true);
        //
        aiFollow.enabled = false;
        aiFollow.navMeshAgent.enabled = false;
    }
    void DisableControlRobot()
    {
        robotController.enabled = false;
        robot.enabled = false;
        robotDetec.enabled = false;
        camRobot.gameObject.SetActive(false);
        //
        aiFollow.enabled = true;
        aiFollow.navMeshAgent.enabled = true;
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
