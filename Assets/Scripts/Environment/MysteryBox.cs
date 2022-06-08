using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public GameObject ParticleSystem;
    public List<GameObject> PossibleBonuses;
    public List<int> BonusWeight;
    public float Chance;

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
            if (Random.Range(0f, 100f) < Chance)
            {
                int rand = Random.Range(0, BonusWeight.Sum() + 1);
                int index = -1;

                for (int c = 0, s = 0; c < BonusWeight.Count; c++)
                {
                    if (rand < s)
                    {
                        index = c - 1;
                        break;
                    }

                    s += BonusWeight[c];
                }
                if (index == -1)
                    index = BonusWeight.Count - 1;


                Instantiate(
                    PossibleBonuses[index],
                    transform.position,
                    Quaternion.Euler(0, 0, Random.Range(0, 360)));                                
            }
            Instantiate(ParticleSystem, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
}
