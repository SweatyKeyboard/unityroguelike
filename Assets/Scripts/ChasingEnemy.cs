using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{

    Player target;
    public float Speed;
    public int Health;
    public GameObject DeadEffect;
    public GameObject DeadParticles;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindObjectOfType<Player>();
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
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            Health -= 25;
        }
    }


}
