using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(int floorType)
    {
        Color c = Color.white;

        if (floorType == 0)        
            c = new Color(0.4f, 0.5f, 0.7f);        
        else if (floorType == 1)
            c = new Color(0.45f, 0.7f, 0.4f);
        else if (floorType == 2)
            c = new Color(0.7f, 0.4f, 0.4f);
        else if (floorType == -1)
            c = new Color(0.7f, 0.6f, 0.4f);
        
        GetComponent<Image>().color = c;
    }
}
