using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Active ActionUI")]
    public bool InteractUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    private void Update()
    {
        ControlActionUI();
    }

    public void ControlActionUI()
    {
        if (!InteractUI)
        {
            Singleton.Instance.actionUI.gameObject.SetActive(true);
        }
        else
        {
            Singleton.Instance.actionUI.gameObject.SetActive(false);
        }
    }
}
