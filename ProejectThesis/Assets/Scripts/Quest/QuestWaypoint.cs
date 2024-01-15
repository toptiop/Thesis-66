using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestWaypoint : MonoBehaviour
{
    public Image img;
    public Vector3 target;
    public Transform playerTransform;
    public TMP_Text meter;
    public Vector3 offset;

    [Header("Enable | Disable")]
    public bool isActive = true;
    void Update()
    {
        
        float minX = img.GetPixelAdjustedRect().width;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height ;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target + offset);

        if (Vector3.Dot((target - transform.position).normalized, transform.position - transform.position) < 0)
        {
            if (pos.x < Screen.width)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        meter.text = ((int)Vector3.Distance(target, playerTransform.position)).ToString() + "m";
    }

    public void SetPoint(Vector3 pos)
    {
        target = pos;
    }

    public void ToggleWaypoint(bool newActive)
    {
        isActive = newActive;

        if(isActive)
        {
            img.gameObject.SetActive(true);
        }
        else
        {
            img.gameObject.SetActive(false);
        }
    }
}
