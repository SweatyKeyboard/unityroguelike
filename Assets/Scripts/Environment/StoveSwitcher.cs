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
        ButtonClick b = FindObjectOfType<ButtonClick>();
        if (collision.CompareTag("Player") && readyToClick &&
            ((Application.isMobilePlatform && b.IsCliked && b.Key == KeyCode.Space) ||
            (!Application.isMobilePlatform) && Input.GetKey(KeyCode.Space)))
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
