using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSpawner : MonoBehaviour
{

    public List<GameObject> Items;
    public List<int> Weights;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Activate()
    {
        int rand = Random.Range(0, Weights.Sum() + 1);
        int index = -1;

        for (int c = 0, s = 0; c < Weights.Count; c++)
        {
            if (rand < s)
            {
                index = c - 1;
                break;
            }

            s += Weights[c];
        }
        if (index == -1)
            index = Weights.Count - 1;

        GameObject g = Instantiate(Items[index], transform.position, transform.rotation);
        g.GetComponent<SpriteRenderer>().sortingOrder = -2;
        g.GetComponent<ShopItem>().IsForSale = true;
        

        Destroy(gameObject);
    }

    public void ActivateOld()
    {
        Destroy(gameObject);
    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
