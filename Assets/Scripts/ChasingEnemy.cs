using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{

    Player target;
    public float Speed;
    public float Health;
    public GameObject DeadEffect;
    public GameObject DeadParticles;

    bool active = false;
    GameController game;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameController>();
        target = FindObjectOfType<Player>();
        Invoke("Activate", 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Instantiate(DeadEffect, transform.position, transform.rotation);
            Instantiate(DeadParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (active)
            Move();
    }

    void Activate()
    {
        active = true;
    }

    void Move()
    {
        Vector3 difference = target.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(rotZ, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            Health -= target.Damage;
        }
    }

    private void OnDestroy()
    {
        game.AreEnemiesDead();        
    }


}
