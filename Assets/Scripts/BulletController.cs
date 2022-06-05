using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float Lifetime;
    public GameObject MissParticles;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(Lifetime);        
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(MissParticles, transform.position, transform.rotation);
    }
}
