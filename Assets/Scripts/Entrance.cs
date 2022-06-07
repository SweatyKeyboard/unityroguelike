using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entrance : MonoBehaviour
{
    public bool Active;
    public int XMod, YMod;
    public int Id;

    GameController game;

    void Start()
    {
        game = FindObjectOfType<GameController>();
    }

    void Update()
    {
        
    }

    public void Deactivate()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        Active = false;
    }

    public void Activate()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        Active = true;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Active && collision.CompareTag("Player") && Input.GetKey(KeyCode.Space))
        {
            Deactivate();
            game.WalkToStart(XMod, YMod, Id);
        }
    }
}
