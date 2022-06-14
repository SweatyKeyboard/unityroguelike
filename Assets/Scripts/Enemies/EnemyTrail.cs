using System.Collections;
using UnityEngine;

public class EnemyTrail : MonoBehaviour
{
    [SerializeField] string trailName;
    [SerializeField] float length;

    public float Length { get { return length; } set { length = value; } }

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<ParticleSystem>().startLifetime = length;
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(length);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<Player>().Hurt(trailName);
        }
    }
}
