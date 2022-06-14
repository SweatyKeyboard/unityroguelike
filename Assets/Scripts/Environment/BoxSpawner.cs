using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{

    [SerializeField] float chance;
    [SerializeField] List<GameObject> box;
    [SerializeField] bool lessChance = true;

    public void Activate()
    {
        if (lessChance)
            chance /= 1.5f * (FindObjectsOfType<MysteryBox>().Length + 0.25f);

        Quaternion angle = Quaternion.Euler(0, 0, Random.Range(0f,360f));

        int type = Random.Range(0, box.Count);

        if (Random.Range(0f, 100f) < chance)
            Instantiate(box[type], transform.position, angle);
        Destroy(gameObject);
    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
