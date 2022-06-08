using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCreator : MonoBehaviour
{

    public GameObject Trail;
    public bool Once;
    public float RepeatTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeTrail()
    {
        Instantiate(Trail, transform.position, transform.rotation);
    }
}
