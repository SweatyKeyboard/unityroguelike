using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutlerySpawner : MonoBehaviour
{
    public List<GameObject> Possible;
    public int choosenInd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Activate()
    {
        choosenInd = Random.Range(0, Possible.Count);
        Instantiate(Resources.Load<GameObject>("Objects/Cutlery" + choosenInd), GameObject.FindGameObjectWithTag("Room").transform);
        Destroy(gameObject);
    }

    public void ActivateOld(int ind)
    {
        choosenInd = ind;
        Instantiate(Resources.Load<GameObject>("Objects/Cutlery" + choosenInd), GameObject.FindGameObjectWithTag("Room").transform);
        Destroy(gameObject);
    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
