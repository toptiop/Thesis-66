using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public List<GameObject> uiActive = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        CheckUIActive();
    }

    public void CheckUIActive()
    {
        if(GameManager.Instance.pauseGame)
        {
            RemoveUI();
            Singleton.controller.canMove = false;
            Singleton.Instance.invUI.hub.SetActive(true);
        }
    }
    public void AddUI(GameObject ui)
    {
        uiActive.Add(ui);
    }

    public void RemoveUI()
    {
        foreach(GameObject go in uiActive)
        {
            go.SetActive(false);
            GetInterface openUI = go.GetComponent<GetInterface>();
            if (openUI != null)
            {
                openUI.SetClose();
                Debug.Log("Set False");
            }
        }
        uiActive.Clear();
    }
}
