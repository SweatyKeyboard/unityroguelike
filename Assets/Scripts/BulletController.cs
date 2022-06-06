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
}
