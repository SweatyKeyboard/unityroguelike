using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText(string text, float time)
    {
        GetComponent<Text>().text = text;
        GetComponent<Text>().color = Color.white;
        Invoke("HideText", time);
    }

    void HideText()
    {
        GetComponent<Text>().color = new Color(1, 1, 1, 0);
    }

}
