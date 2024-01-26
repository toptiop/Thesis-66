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
    public bool shop;
    public bool interaction;
    public bool oneSlot;
    public bool twoSlot;
    public bool threeSlot;
    public bool fourSlot;
    public bool fiveSlot;
    public bool sixSlot;
    public bool esc;
    public bool mouse0;
    public bool mouse1;
    public bool scrollIncrease;
    public bool scrollDecrease;
    public bool book;
    public bool mouseHold;


    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;


    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
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

    public void OnShop(InputValue value)
    {
        ShopInput(value.isPressed);
    }

    public void OnInteraction(InputValue value)
    {
        InteractionInput(value.isPressed);
    }
    public void OnOneSlot(InputValue value)
    {
        OneSlotInput(value.isPressed);
    }
    public void OnTwoSlot(InputValue value)
    {
        TwoSlowInput(value.isPressed);
    }
    public void OnThreeSlot(InputValue value)
    {
        ThreeSlotInput(value.isPressed);
    }
    public void OnFourSlot(InputValue value)
    {
        FourSlotInput(value.isPressed);
    }
    public void OnFiveSlot(InputValue value)
    {
        FiveSlotInput(value.isPressed);
    }
    public void OnSixSlot(InputValue value)
    {
        SixSlotInput(value.isPressed);
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
    public void OnScrollIncrease(InputValue value)
    {
        ScrollIncreaseInput(value.isPressed);
    }
    public void OnScrollDecrease(InputValue value)
    {
        ScrollDecreaseInput(value.isPressed);
    }   
    public void OnBook(InputValue value)
    {
        BookInput(value.isPressed);
    }
    public void OnMouseHold(InputValue value)
    {
        MouseHoldInput(value.isPressed);
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

    public void ShopInput(bool newShopState)
    {
        shop = newShopState;
    }

    public void InteractionInput(bool newInteractionState)
    {
        interaction = newInteractionState;
    }
    public void OneSlotInput(bool newOneSlotState)
    {
        oneSlot = newOneSlotState;
    }
    public void TwoSlowInput(bool newTwoSlowState)
    {
        twoSlot = newTwoSlowState;
    }
    public void ThreeSlotInput(bool newThreeSlotState)
    {
        threeSlot = newThreeSlotState;
    }
    public void FourSlotInput(bool newFourSlotState)
    {
        fourSlot = newFourSlotState;
    }
    public void FiveSlotInput(bool newFiveSlotState)
    {
        fiveSlot = newFiveSlotState;
    }
    public void SixSlotInput(bool newSixSlotState)
    {
        sixSlot = newSixSlotState;
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
    public void ScrollIncreaseInput(bool newScrollIncreaseState)
    {
        scrollIncrease = newScrollIncreaseState;
    }    
    public void ScrollDecreaseInput(bool newScrollDecreaseState)
    {
        scrollDecrease = newScrollDecreaseState;
    }    
    public void BookInput(bool newBookState)
    {
        book = newBookState;
    }
    public void MouseHoldInput(bool newMouseHoldState)
    {
        mouseHold = newMouseHoldState;
    }
}