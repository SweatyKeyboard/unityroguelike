using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public GameObject SmokeParticles;
    public bool TurnedOn;

    bool pause = false;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        if (TurnedOn)
        {
            GetComponent<SpriteRenderer>().sprite = 
                Resources.Load<Sprite>("StoveOn");
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("StoveOff");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!pause && collision.CompareTag("Player") && TurnedOn)
        {
            Instantiate(SmokeParticles, player.transform.position, transform.rotation);
            FindObjectOfType<Player>().Hurt("Плита");
            pause = true;
            Invoke("Unpause", 0.5f);
        }
        else if (!pause && collision.CompareTag("Enemy") && TurnedOn)
        {
            Instantiate(SmokeParticles, collision.transform.position, collision.transform.rotation);
            collision.gameObject.GetComponent<Enemy>().Hurt(2, 0.5f);
            pause = true;
            Invoke("Unpause", 0.5f);
        }
    }

    void Unpause()
    {
        pause = false;
    }

    public void Change()
    {
        TurnedOn = !TurnedOn;

        if (TurnedOn)
        {
            GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("StoveOn");
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("StoveOff");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
