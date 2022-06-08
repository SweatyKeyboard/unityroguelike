using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public float Health;
    public GameObject DeadEffect;
    public GameObject DeadParticles;

    protected bool active = false;
    protected Player target;

    bool invulnerable;

    public virtual void Start()
    {
        target = FindObjectOfType<Player>();
        Invoke("Activate", 0.8f);
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
    }

    private void OnDestroy()
    {
        FindObjectOfType<LevelController>().AreEnemiesDead();
    }
}
