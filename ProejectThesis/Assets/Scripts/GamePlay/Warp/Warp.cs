using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour, IInteractable
{
    public PlayerController player;
  
    public GameObject fadeScreen;
    private Animator anim;
    public Transform warpPos;
    public Transform robotWarp;
    public AutoPilotRobot robot;
    public bool warpBot;

    public BoxCollider col;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        robot = FindObjectOfType<AutoPilotRobot>();

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
        player.canMove = true;
        col.enabled = false;
        player.enabled = false;

        if(warpBot)
        {
            robot.enabled = false;
            robot.agent.enabled = false;
        }

        fadeScreen.SetActive(true);
        anim.Play("Fade In");

        yield return new WaitForSeconds(2);

        
        player.transform.position = warpPos.position;
        if (warpBot)
        {
            robot.transform.position = warpPos.position;
        }
        yield return new WaitForSeconds(1);
        anim.Play("Fade Out");

        
        fadeScreen.SetActive(false);

        col.enabled = true;
        player.enabled = true;
        player.canMove = false;
        if(warpBot)
        {
            robot.enabled = true;
            robot.agent.enabled = true;
        }
    }
}
