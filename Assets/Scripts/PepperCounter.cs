using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PepperCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int pepper = 0;
        if (PlayerPrefs.HasKey("Pepper"))
            pepper = PlayerPrefs.GetInt("Pepper");

        GetComponent<Text>().text = pepper.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
