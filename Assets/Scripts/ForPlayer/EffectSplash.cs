using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSplash : MonoBehaviour
{
    [SerializeField] Common.Effects effect;
    public GameObject System { get; set; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!collision.GetComponent<Enemy>().Effects.Contiains(effect))
            collision.GetComponent<Enemy>().AddEffect(effect, 3f);
            StartCoroutine(DeathDelay());
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
