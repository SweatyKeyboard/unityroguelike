using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] float lifetime;
    [SerializeField] GameObject particles;
    [SerializeField] GameObject splash;
    [SerializeField] AudioClip splashSound;

    public float Lifetime
    {
        get { return lifetime; }
        set { lifetime = value; }
    }

    void Start()
    {
        StartCoroutine(DeathDelay());
    }
    IEnumerator DeathDelay()
    {        
        yield return new WaitForSeconds(lifetime);        
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(splashSound, new Vector3(0, 0, -10));
        Instantiate(particles, transform.position, transform.rotation);
        Instantiate(splash, transform.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (! (collision.gameObject.CompareTag("Particles") ||
               collision.gameObject.CompareTag("PlayerTrail") ||
               collision.gameObject.CompareTag("EnemyTrail") ||
               collision.gameObject.CompareTag("Player") ||
               collision.gameObject.CompareTag("Enemy")))
            Destroy(gameObject);
    }


}
