using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> types;

    public void Activate()
    {
        FindObjectOfType<LevelController>().NewEnemies();
        GameObject g = Instantiate(types[Random.Range(0, types.Count)], transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
