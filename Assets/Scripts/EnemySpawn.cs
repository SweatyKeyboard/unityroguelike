using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<GameObject> types;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activete()
    {
        FindObjectOfType<GameController>().NewEnemies();
        Instantiate(types[UnityEngine.Random.Range(0, types.Count)], transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
