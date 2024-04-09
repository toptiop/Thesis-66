using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerState : MonoBehaviour
{
    [Header("Interacting")]
    public bool isInteractingBox;
    public Animator animator;
    public Rig handRig;

    public void AnimationInteractingBox(bool newIsInteractingBox)
    {
        isInteractingBox = !newIsInteractingBox;
        animator.SetBool("IsPickup", newIsInteractingBox);

        if(!isInteractingBox )
        {
            animator.SetLayerWeight(1, 1);
            handRig.weight = 1;
        }
        else
        {
            animator.SetLayerWeight(1, 0);
            handRig.weight = 0;
        }
    }
}
