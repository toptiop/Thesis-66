using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryInfo : MonoBehaviour
{
    public static InventoryInfo instance;
    public Image imageIcon;
    public TMP_Text title;
    public TMP_Text description;

    private Inventory inv;
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

        inv = FindAnyObjectByType<Inventory>();
    }
   
    public void SetupInfoDisplay(SO_Item item) 
    {
        if(item != inv.EMPTY_ITEM)
        {
            imageIcon.gameObject.SetActive(true);
            imageIcon.sprite = item.icon;
            title.text = item.itemName;
            description.text = item.description;
        }
        else
        {
            imageIcon.gameObject.SetActive(false);
            title.text = "";
            description.text = "";
        }

    }

    public void SetupNull()
    {
        imageIcon.gameObject.SetActive(false);
        title.text = "";
        description.text = "";
    }
}
