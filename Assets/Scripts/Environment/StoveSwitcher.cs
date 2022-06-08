using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSwitcher : MonoBehaviour
{
    public GameObject LinkedStove;

    bool readyToClick = true;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.Space) && readyToClick)
        {
            LinkedStove.GetComponent<Stove>().Change();
            readyToClick = false;
            Invoke("Ready", 0.5f);
        }
    }

    void Ready()
    {
        readyToClick = true;
    }
}
