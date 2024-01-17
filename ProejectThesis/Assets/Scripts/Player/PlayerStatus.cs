using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    public float currentFood = 0;
    public float maxFood = 100;
    public float decreaseFood = 2.0f;
    public float timer = 10;

    public Image imgFood;
    public TMP_Text foodText;

    private void Start()
    {
        currentFood = maxFood;
    }


    void Update()
    {
        currentFood = Mathf.Clamp(currentFood, 0f, maxFood);

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            DelFood(decreaseFood);
            timer = 10f; 
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        float fillAmount = currentFood / maxFood;
        if (imgFood != null && foodText != null)
        {
            imgFood.fillAmount = fillAmount;
            foodText.text = currentFood + "|" + maxFood.ToString(); ;
        }
    }

    public void AddFood(float value)
    {
        currentFood += value;
    }

    public void DelFood(float value)
    {
        currentFood -= value;
    }
}
