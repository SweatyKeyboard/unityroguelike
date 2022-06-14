using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour
{
    public Text Counter;
    public int Points;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("CharacterPoints"))
            Points = PlayerPrefs.GetInt("CharacterPoints") + 3;
        else
            Points = 3;

        ForceUpdate();
    }

    public void ForceUpdate()
    {
        Counter.text = "Points last: " + Points;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
