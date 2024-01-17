using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionUI : MonoBehaviour
{
    [SerializeField]Canvas _canvas;
    public TMP_Text ActionText;
    public GameObject textPress;

    void Awake()
    {
        _canvas = GetComponent<Canvas>(); // ดึง Canvas ที่อยู่ใน GameObject นี้ (ActionUICanvas)
    }

   

    // แสดง canvas
    public void ShowCanvas()
    {
        if (!_canvas.enabled)
            _canvas.enabled = true;

        textPress.SetActive(true);
    }

    // ซ่อน canvas
    public void HideCanvas()
    {
        if (_canvas.enabled)
            _canvas.enabled = false;

        textPress.SetActive(false);
    }

    // แสดง canvas พร้อม action text ที่ต้องการ
    public void ShowInteractionUI(string interactionText)
    {
        ShowCanvas();
        ActionText.text = interactionText; // แสดง ActionText
    }
}
