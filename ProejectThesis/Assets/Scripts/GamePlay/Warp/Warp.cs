using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour, IInteractable
{
    public PlayerController player;
    public RobotController robot;
    public GameObject fadeScreen;
    private Animator anim;
    public Transform warpPos;
    public Transform robotWarp;

    public BoxCollider col;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        robot = FindObjectOfType<RobotController>();

        anim = fadeScreen.GetComponent<Animator>();
        col = GetComponent<BoxCollider>();
    }
    public string GetInteractionText()
    {
        return "Enter";
    }

    public void Interact()
    {
        StartCoroutine(Delaywarp());
    }


    IEnumerator Delaywarp()
    {
        col.enabled = false;
        player.enabled = false;
        robot.enabled = false;

        fadeScreen.SetActive(true);
        anim.Play("Fade In");

        yield return new WaitForSeconds(2);

        
        player.transform.position = warpPos.position;
        robot.transform.position = robotWarp.position;
        yield return new WaitForSeconds(1);
        anim.Play("Fade out");

        
        fadeScreen.SetActive(false);

        col.enabled = true;
        player.enabled = true;
        robot.enabled = true;
    }
}
