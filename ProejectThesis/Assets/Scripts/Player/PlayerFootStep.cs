using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStep : MonoBehaviour
{
    public AudioClip[] FootstepAudioClips;
    public AudioSource audioSource;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

    public void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                audioSource.PlayOneShot(FootstepAudioClips[index], FootstepAudioVolume);
            }
        }
    }
}
