using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipment : MonoBehaviour
{
    public GameObject glass;
    public GameObject glassScifi;

    public void SwapGlass()
    {
        glass.SetActive(false);
        glassScifi.SetActive(true);
    }
}
