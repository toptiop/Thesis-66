using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;


public class Tracker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public VideoPlayer video;
    Slider tracking;
    bool slide = false;

    public Button play;
    public Button pause;

    void Start()
    {
        tracking = GetComponent<Slider>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (!slide & video.isPlaying)
            tracking.value = (float)video.frame / (float)video.frameCount;

        if ((float)video.frame >= (float)video.frameCount - 1)
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
        float frame = (float)tracking.value * (float)video.frameCount;
        video.frame = (long)frame;
        slide = false;
    }
}
