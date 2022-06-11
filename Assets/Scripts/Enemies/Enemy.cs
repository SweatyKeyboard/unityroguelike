using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string EnemyName;
    public float Speed;
    public float Health;
    public int Cost;
    public GameObject DeadEffect;
    public GameObject DeadParticles;

    protected bool active = false;
    protected Player target;

    bool invulnerable;

    public virtual void Start()
    {
        Health += (int)(Math.Pow(1.5, FindObjectOfType<GameController>().currentLevel) * 2) / 2;
        target = FindObjectOfType<Player>();
        Invoke("Activate", 1f);
    }

    public void Update()
    {
        
    }

    public void Hurt(float hp, float invTime)
    {
        if (!invulnerable)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0.8f, 0.8f, 0.7f);
            invulnerable = true;
            Health -= hp;

            if (Health <= 0)
            {
                Instantiate(DeadEffect, transform.position, transform.rotation);
                Instantiate(DeadParticles, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
                Invoke("stillAlive", invTime);


        }
    }

    void stillAlive()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        invulnerable = false;
    }

    public void FixedUpdate()
    {
        if (active)
            Move();
    }

    public void Activate()
    {
        active = true;
    }
    public virtual void Move()
    {

    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            Hurt(target.Damage, 0.1f);
        }
        else if (col.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            target.Hurt(EnemyName);
        }
    }

    private void OnDestroy()
    {
        FindObjectOfType<LevelController>().AreEnemiesDead();
        FindObjectOfType<GameController>().Score += Cost;
        FindObjectOfType<GameController>().EnemiesKilled++;
    }
}
