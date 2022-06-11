using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float Lifetime;
    public GameObject Particles;
    public GameObject Splash;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(Lifetime);        
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(Particles, transform.position, transform.rotation);
        Instantiate(Splash, transform.position, transform.rotation);
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
