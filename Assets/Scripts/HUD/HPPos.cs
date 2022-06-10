using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPPos : MonoBehaviour
{
    Player player;
    int maxContainers;
    HudController hud;
    void Start()
    {
        ForceStart();
    }

    public void ForceStart()
    {
        hud = FindObjectOfType<HudController>();
        if (Application.isMobilePlatform)
            Invoke("GetUpdates", 0.2f);
        else
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
        Vector3 startPos = transform.position;
        GameObject cont = new GameObject();
        cont.tag = "HealthHUD";
        cont.transform.position = new Vector3(startPos.x + 0.1f + pos * 0.4f - (pos / 5) * 2f, startPos.y - 0.1f - (pos / 5) * 0.9f, 1);
        cont.transform.parent = transform.parent;
        cont.AddComponent<Image>().sprite = Resources.Load<Sprite>(sprite);
        cont.GetComponent<RectTransform>().anchorMin.Set(0, 1);
        cont.GetComponent<RectTransform>().anchorMax.Set(0, 1);
        cont.transform.localScale = new Vector3(0.55f, 0.55f);
    }


    void Update()
    {
        
    }
}
