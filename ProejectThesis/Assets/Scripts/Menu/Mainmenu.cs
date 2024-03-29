using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Mainmenu : MonoBehaviour
{
    public GameObject panelEscape;
    GameManager gm;
    [SerializeField] private InputManager _input;
    void Start()
    {
        gm = GameManager.Instance;
        gm.TogglePause(false);
        gm.ChangeStateInteractUI(true);

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
        gm.pauseGame = !gm.pauseGame;

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
