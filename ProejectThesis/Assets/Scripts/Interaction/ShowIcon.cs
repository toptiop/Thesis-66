using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIcon : MonoBehaviour
{
    public Canvas _canvas;

    public Image icon;

    public Sprite canInteract;
    public Sprite cantInteract;
    public GameObject text;

    private void Awake()
    {
        if(_canvas == null)
            _canvas = GetComponentInChildren<Canvas>();
    }

    public void ActiveIcon()
    {
        Debug.Log("Enabled");
        if (!_canvas.enabled)
            _canvas.enabled = true;
        icon.gameObject.SetActive(true);
    }

    public void HideIcon()
    {
        Debug.Log("Disabled");
        if (_canvas.enabled)
            _canvas.enabled = false;
        icon.gameObject.SetActive(false);
    }

    public void ChangeIcon()
    {
        icon.sprite = canInteract;
        text.SetActive(true);
    }

    public void ChangeCantIcon()
    {
        icon.sprite = cantInteract;
        text.SetActive(false);
    }
}
