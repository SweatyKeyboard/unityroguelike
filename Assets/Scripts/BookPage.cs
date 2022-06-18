using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookPage : MonoBehaviour
{
    void Start()
    {
        
    }

    public void SetImage(string image)
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Encyclopedia/" + image);
    }

    public void MakeTransparent()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
}
