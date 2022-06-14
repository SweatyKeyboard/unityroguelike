using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopItemSpawner : MonoBehaviour
{

    public List<GameObject> Items;
    public List<int> Weights;
    public int IndexInRoom;

    public void Activate()
    {
        int rand = UnityEngine.Random.Range(0, Weights.Sum() + 1);
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

        GameObject g = Instantiate(Items[index], transform.position, transform.rotation, transform.parent);
        g.GetComponent<SpriteRenderer>().sortingOrder = -2;
        g.GetComponent<ShopItem>().IsForSale = true;
        g.AddComponent<Interactive>().Type = Common.InteractiveType.ShopItem;
        g.GetComponent<Interactive>().Index = IndexInRoom;
        g.transform.localScale = new Vector3(0.4f,0.4f,0);
        

        Destroy(gameObject);
    }

    public void ActivateOld(string objName)
    {
        objName = objName.Substring(0, objName.IndexOf('('));
        int index = Convert.ToInt32(new string(objName.Where(x => char.IsDigit(x)).ToArray()));

        GameObject g = Instantiate(
            Resources.Load<GameObject>("Items/"+objName),
            transform.position, transform.rotation,
            transform.parent);
        g.GetComponent<SpriteRenderer>().sortingOrder = -2;
        g.GetComponent<ShopItem>().IsForSale = true;
        g.AddComponent<Interactive>().Type = Common.InteractiveType.ShopItem;
        g.GetComponent<Interactive>().Index = IndexInRoom;
        g.transform.localScale = new Vector3(0.4f, 0.4f, 0);

        Destroy(gameObject);
    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
