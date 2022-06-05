using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public List<GameObject> types;

    // Start is called before the first frame update
    void Start()
    {
        System.Random r = new System.Random();
        Instantiate(types[r.Next(types.Count)], transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
