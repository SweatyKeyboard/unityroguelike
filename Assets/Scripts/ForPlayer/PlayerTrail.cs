using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    public float Length;

    bool pause;

    void Start()
    {
        GetComponentInChildren<ParticleSystem>().startLifetime = Length;
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(Length);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy") && !pause)
        {
            collision.gameObject.GetComponent<Enemy>().Hurt(2, 0.5f);
            pause = true;
            Invoke("Unpause", 0.5f);
        }
    }

    void Unpause()
    {
        pause = false;
    }
}
