using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public float Health;
    public GameObject DeadEffect;
    public GameObject DeadParticles;

    protected bool active = false;
    protected GameController game;
    protected Player target;

    public virtual void Start()
    {
        game = FindObjectOfType<GameController>();
        target = FindObjectOfType<Player>();
        Invoke("Activate", 0.8f);
    }

    public void Update()
    {
        if (Health <= 0)
        {
            Instantiate(DeadEffect, transform.position, transform.rotation);
            Instantiate(DeadParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
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
            Health -= target.Damage;
        }
    }

    private void OnDestroy()
    {
        game.AreEnemiesDead();
    }
}
