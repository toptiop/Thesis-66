using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool inventory;
    public bool swap;
    public bool interaction;
    public bool esc;
    public bool mouse0;
    public bool mouse1;


    public bool next;
    public bool back;

    //Command
    public bool command;
    public bool returnPlayer;


    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;


    public void OnMove(InputValue value)
    {
        if(Singleton.Instance != null)
        {
            if (!Singleton.controller.canMove)
            {
                MoveInput(value.Get<Vector2>());
            }
            else
            {
                move = Vector2.zero;
            }
        }
        else
        {
            MoveInput(value.Get<Vector2>());
        }
    }

    public void OnLook(InputValue value)
    {

        if (GameManager.Instance.cameraMove)
        {
            look = Vector2.zero;
        }
        else
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void OnInventory(InputValue value)
    {
        InventoryInput(value.isPressed);
    }

    public void OnSwap(InputValue value)
    {
        SwapInput(value.isPressed);
    }

    public void OnInteraction(InputValue value)
    {
        InteractionInput(value.isPressed);
    }

    public void OnESC(InputValue value)
    {
        ESCInput(value.isPressed);
    }
    public void OnMouse0(InputValue value)
    {
        Mouse0Input(value.isPressed);
    }
    public void OnMouse1(InputValue value)
    {
        Mouse1Input(value.isPressed);
    }
    public void OnNextDialogue(InputValue value)
    {
        NextDialogueInput(value.isPressed);
    }
    public void OnBackDialogue(InputValue value)
    {
        BackDialogueInput(value.isPressed);
    }
    public void OnCommand(InputValue value)
    {
        CommandInput(value.isPressed);
    }
    public void OnReturn(InputValue value)
    {
        ReturnInput(value.isPressed);
    }

    //


    //
    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    public void InventoryInput(bool newInventoryState)
    {
        inventory = newInventoryState;
    }

    public void SwapInput(bool newSwapState)
    {
        swap = newSwapState;
    }

    public void InteractionInput(bool newInteractionState)
    {
        interaction = newInteractionState;
    }

    public void ESCInput(bool newESCState)
    {
        esc = newESCState;
    }
    public void Mouse0Input(bool newMouse0State)
    {
        mouse0 = newMouse0State;
    }
    public void Mouse1Input(bool newMouse1State)
    {
        mouse1 = newMouse1State;
    }
    public void NextDialogueInput(bool newNextDialogueState)
    {
        next = newNextDialogueState;
    }
    public void BackDialogueInput(bool newBackDialogueState)
    {
        back = newBackDialogueState;
    }

    public void CommandInput(bool newCommandState)
    {
        command = newCommandState;
    }

    public void ReturnInput(bool newReturnState)
    {
        returnPlayer = newReturnState;
    }
}