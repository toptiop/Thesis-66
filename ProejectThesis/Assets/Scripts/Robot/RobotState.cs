using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotState : MonoBehaviour
{
    [Header("Interacting")]
    public bool isInteractingBox;
    public Animator animator;
    public void AnimationInteractingBox(bool newIsInteractingBox)
    {
        isInteractingBox = !newIsInteractingBox;
        animator.SetBool("IsPickup", newIsInteractingBox);
    }
}
