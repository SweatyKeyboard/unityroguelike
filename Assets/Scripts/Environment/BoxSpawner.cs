using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{

    public float Chance;
    public GameObject Box;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        Chance /= 2 * (FindObjectsOfType<MysteryBox>().Length + 0.5f);
        Quaternion angle = Quaternion.Euler(0, 0, Random.Range(0f,360f));

        if (Random.Range(0f, 100f) < Chance)
            Instantiate(Box, transform.position, angle);
        Destroy(gameObject);
    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
