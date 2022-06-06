using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{

    public GameObject ParticleSystem;
    public List<GameObject> PossibleBonuses;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            System.Random r = new System.Random();

            Instantiate(
                PossibleBonuses[r.Next(PossibleBonuses.Count)],
                transform.position,
                Quaternion.Euler(0, 0, r.Next(360)));
            Instantiate(ParticleSystem, transform.position, transform.rotation);

            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
}
