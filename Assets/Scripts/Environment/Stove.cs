using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    [SerializeField] GameObject SmokeParticles;
    [SerializeField] bool turnedOn;
    [SerializeField] AudioClip Sound;

    public bool TurnedOn 
    { 
        get
        { 
            return turnedOn;
        } 
    set
        {
            turnedOn = value;
        }
    }

    bool pause = false;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = Sound;
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
            CollisionedWithEntity(collision);
            FindObjectOfType<Player>().Hurt("Stove");
            
        }
        else if (!pause && collision.CompareTag("Enemy") && TurnedOn)
        {
            CollisionedWithEntity(collision);
            collision.gameObject.GetComponent<Enemy>().Hurt(2, 0.5f);            
        }
    }

    void CollisionedWithEntity(Collider2D collision)
    {
        Instantiate(SmokeParticles, collision.transform.position, collision.transform.rotation);
        pause = true;
        Invoke("Unpause", 0.5f);
        GetComponent<AudioSource>().Play();
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
