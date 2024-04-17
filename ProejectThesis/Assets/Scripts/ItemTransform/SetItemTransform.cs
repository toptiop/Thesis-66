using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItemTransform : MonoBehaviour
{
    public ItemTransform itemTransform = new ItemTransform();
    // Update is called once per frame
    void Update()
    {
        itemTransform.Position = transform.position;

        Quaternion quaternion = transform.rotation;
        itemTransform.Rotation = quaternion;    
    }
}
