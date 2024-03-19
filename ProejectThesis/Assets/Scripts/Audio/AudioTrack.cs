using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class AudioTrack : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public AudioSource audioSource;
    Slider tracking;
    bool slide = false;

    public Button play;
    public Button pause;

    void Start()
    {
        tracking = GetComponent<Slider>();
    }

    void Update()
    {
        if (!slide && audioSource.isPlaying)
            tracking.value = audioSource.time / audioSource.clip.length;

        if(audioSource.time >= audioSource.clip.length -1)
        {
            play.gameObject.SetActive(true);
            pause.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        slide = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float timeInSeconds = tracking.value * audioSource.clip.length;
        audioSource.time = timeInSeconds;
        slide = false;
    }

}
