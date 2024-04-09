using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotActiveCommand : MonoBehaviour
{
    public Commander typeCommand;

    public Vector3 setPos;
    public Quaternion setRotation;


    [Space]
    [Header("HackDoor")]
    [SerializeField] DoorHack doorHacking;
    [SerializeField] private bool isHacking = false;

    [Space]
    [Header("ActiveSwitch")]
    [SerializeField] private DoorActive door;
    [SerializeField] private bool isActiveObj;
}
