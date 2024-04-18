using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{

    public GameObject screenDie;
    public Animator anim;

    public PlayerController player;
    public AutoPilotRobot ai;
    private void Start()
    {
        anim = screenDie.GetComponent<Animator>();

        player = FindObjectOfType<PlayerController>();
        ai = FindObjectOfType<AutoPilotRobot>();
    }


    public IEnumerator DIE()
    {
        Debug.Log("Die");
        screenDie.SetActive(true);

        player.enabled = false;
        ai.enabled = false;

        anim.Play("YouDie");

        yield return new WaitForSeconds(2.5f);

        Singleton.Instance.checkPoint.RespawnPlayer(player.gameObject);
        Singleton.Instance.checkPoint.RespawnPlayer(ai.gameObject);

        yield return new WaitForSeconds(1);

        anim.Play("YouDie_Cl");

        screenDie.SetActive(false);

        player.enabled = true;
        ai.enabled = true;
    }
}
