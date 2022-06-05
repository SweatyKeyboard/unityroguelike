using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        for (int c = 0; c < 6; c++)
        {
           player.AddHP(Common.HealthType.Ketchup);
        }

        FindObjectOfType<HudController>().UpdateHP();
    }

    void Update()
    {
        
    }
}
