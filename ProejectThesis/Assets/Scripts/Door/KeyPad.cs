using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPad : MonoBehaviour
{
    public string correctPassword = "1234";
    [SerializeField]private string enteredPassword = "";

    public Image image;
    public TMP_Text mP_Text;
    public GameObject uiCanvas;
    public bool isUnlock;

    public Color current, correct, incorrect;
    public void KeyPadPass(string pass)
    {
        enteredPassword += pass;
        UpdateUI();
    }
    public void KeyPadDel()
    {
        if (enteredPassword.Length > 0)
        {
            enteredPassword = enteredPassword.Substring(0, enteredPassword.Length - 1);
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        if (mP_Text != null)
        {
            mP_Text.text = enteredPassword;
        }
    }

    public void CheckPassword()
    {
        if (enteredPassword == correctPassword)
        {
            Debug.Log("Correct password! Door unlocked.");
            isUnlock = true;
            image.color = correct;
            uiCanvas.SetActive(false);
            GameManager.Instance.ChangeStateInteractUI(false);
            // Add your code to unlock the door or perform any other action here
        }
        else
        {
            Debug.Log("Incorrect password! Try again.");
            enteredPassword = ""; // Reset the entered password
            image.color = incorrect;
            StartCoroutine(swapColor());
        }
    }

    IEnumerator swapColor()
    {
        yield return new WaitForSeconds(1.5f);
        image.color = current;
    }
}
