using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Common.ItemType Type;

    GameController game;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Type == Common.ItemType.HealthK)
            {
                FindObjectOfType<Player>().AddHP(Common.HealthType.Ketchup);
                FindObjectOfType<Player>().AddHP(Common.HealthType.Ketchup);
                FindObjectOfType<HudController>().UpdateHP();
                
            }
            else if (Type == Common.ItemType.Salt)
            {
                game.Salt += Random.Range(1, 3);
                GameObject.Find("SaltCounter").GetComponent<Text>().text = game.Salt.ToString();
            }

            Destroy(gameObject);
        }
    }
}
