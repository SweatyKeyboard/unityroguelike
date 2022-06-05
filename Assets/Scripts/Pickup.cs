using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Common.ItemType Type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Type == Common.ItemType.HealthK)
            {
                FindObjectOfType<Player>().AddHP(Common.HealthType.Ketchup);
                FindObjectOfType<Player>().AddHP(Common.HealthType.Ketchup);
                FindObjectOfType<HudController>().UpdateHP();
                Destroy(gameObject);
            }
        }
    }
}
