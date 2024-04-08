using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface OpenUI
{
    public bool IsOpen { get; set; }
    void Close();
}