using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPos : MonoBehaviour
{
    Player player;
    int maxContainers;
    HudController hud;
    void Start()
    {
        hud = FindObjectOfType<HudController>();
        GetUpdates();        
    }

    public void GetUpdates()
    {
        maxContainers = FindObjectOfType<Player>().MaxHealth / 2;  

        for (int c = GameObject.FindGameObjectsWithTag("HealthHUD").Length; c < maxContainers; c++)
        {
            AddHeart(c, "EmptyHealthBar");
        }

        hud.UpdateHP();
    }

    public void AddHeart(int pos, string sprite)
    {
        GameObject cont = new GameObject();
        cont.tag = "HealthHUD";
        cont.transform.localScale = new Vector3(0.6f, 0.6f);
        cont.transform.position = new Vector3(transform.position.x + pos * 0.33f - (pos / 5) * 1.66f, transform.position.y - (pos / 5) * 0.74f);
        cont.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprite);
        cont.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }


    void Update()
    {
        
    }
}
