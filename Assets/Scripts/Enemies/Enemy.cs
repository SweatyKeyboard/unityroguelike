using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] string EnemyName;
    [SerializeField] protected float Speed;
    [SerializeField] float Health;
    [SerializeField] int Cost;
    [SerializeField] GameObject DeadEffect;
    [SerializeField] GameObject DeadParticles;
    [SerializeField] AudioClip livingSound;
    [SerializeField] AudioClip hurtSound;

    protected bool active = false;
    protected Player target;
    bool invulnerable;

    EffectList effects = new EffectList();
    Color color = new Color(1, 1, 1, 1);

    public EffectList Effects { get { return effects; } }

    public virtual void Start()
    {
        Health += (int)(Math.Pow(1.5, GameController.FloorsCompleted) * 2) / 3;
        Speed += (int)(Math.Pow(1.5, GameController.FloorsCompleted) * 2) / 3.5f;
        target = FindObjectOfType<Player>();

        float startTime = UnityEngine.Random.Range(0.8f, 1.2f);
        Invoke("Activate", startTime);
    }

    public void Hurt(float hp, float invTime)
    {
        if (!invulnerable)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0.8f, 0.8f, 0.7f);
            invulnerable = true;
            Health -= hp;

            AudioSource.PlayClipAtPoint(hurtSound, new Vector3(0, 0, -10));

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
        GetComponent<SpriteRenderer>().color = color;
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
        StartCoroutine(MakeSound());
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
        GameController.Score += Cost;
        GameController.EnemiesKilled++;
    }

    IEnumerator MakeSound()
    {
        while (true)
        {
            GetComponent<AudioSource>().clip = livingSound;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
        }
    }

    public void AddEffect(Common.Effects effect, float duration)
    {

        switch (effect)
        {
            case Common.Effects.Poison:
                StartCoroutine(EffectPoison(duration));
                break;
            case Common.Effects.Slowdown:
                StartCoroutine(EffectSlowDown(duration));
                break;
        }
    }



    IEnumerator EffectPoison(float dur)
    {
        color = new Color(0.6f, 1, 0.6f, 1);
        GetComponent<SpriteRenderer>().color = color;
        for (int c = 0; c < (int)dur; c++)
        {            
            Hurt(1, 0.5f);
            yield return new WaitForSeconds(1f);
        }
        color = new Color(1, 1, 1, 1);
        GetComponent<SpriteRenderer>().color = color;
    }

    IEnumerator EffectSlowDown(float dur)
    {
        color = new Color(0.8f, 0.6f, 0.4f, 1);
        Speed /= 2;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(dur);
        Speed *= 2;
        color = new Color(1, 1, 1, 1);
        GetComponent<SpriteRenderer>().color = color;
    }
}
