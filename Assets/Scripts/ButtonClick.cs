using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public KeyCode Key;
    public bool IsCliked;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SimulateClick(int i)
    {
        Key = (KeyCode)i;
        IsCliked = true;
        Invoke("ButtonUp", 0.1f);
        //Space - 32
        //Esc - 27
    }

    public void ButtonUp()
    {
        IsCliked = false;
    }
}
