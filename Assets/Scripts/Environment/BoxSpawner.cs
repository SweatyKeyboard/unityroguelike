using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{

    public float Chance;
    public List<GameObject> Box;
    public bool LessChance = true;

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
        if (LessChance)
            Chance /= 1.5f * (FindObjectsOfType<MysteryBox>().Length + 0.25f);

        Quaternion angle = Quaternion.Euler(0, 0, Random.Range(0f,360f));

        int type = Random.Range(0, Box.Count);

        if (Random.Range(0f, 100f) < Chance)
            Instantiate(Box[type], transform.position, angle);
        Destroy(gameObject);
    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
