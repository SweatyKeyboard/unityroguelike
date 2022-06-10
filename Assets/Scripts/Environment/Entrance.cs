using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entrance : MonoBehaviour
{
    public bool Active;
    public int XMod, YMod;
    public int Id;

    void Start()
    {
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
        ButtonClick b = FindObjectOfType<ButtonClick>();
        if (Active && collision.CompareTag("Player") &&
            ((Application.isMobilePlatform && b.IsCliked && b.Key == KeyCode.Space) ||
            (!Application.isMobilePlatform) && Input.GetKey(KeyCode.Space)))
        {
            Deactivate();
            FindObjectOfType<LevelController>().WalkToPart1(XMod, YMod, Id);
        }
    }
}
