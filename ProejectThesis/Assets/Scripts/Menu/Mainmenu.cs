using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Mainmenu : MonoBehaviour
{
    public GameObject panelEscape;
    [SerializeField] private InputManager _input;
    void Start()
    {
        
        GameManager.Instance.TogglePause(false);

        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex == 0)
        {
            GameManager.Instance.ChangeStateInteractUI(true);
        }


        //GetComponent
        _input = FindAnyObjectByType<InputManager>();

        //Set Obj
        if(panelEscape != null )
            panelEscape.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_input != null)
        {
            if (_input.esc)
            {
                PauseGame();
                _input.esc = false;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        // โค้ดออกเกม
        Application.Quit(); // ใช้สำหรับการออกเกมบนแพลตฟอร์มที่รองรับ
    }

    public void PauseGame()
    {
        GameManager.Instance.pauseGame = !GameManager.Instance.pauseGame;

        if(!GameManager.Instance.pauseGame )
        {
            panelEscape.SetActive(false);
            GameManager.Instance.TogglePause(false);
        }
        else
        {
            panelEscape.SetActive(true);
            GameManager.Instance.TogglePause(true);
        }
    }
}
