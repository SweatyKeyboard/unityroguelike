using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CutlerySpawner : MonoBehaviour
{
    public List<GameObject> Possible;
    public int ChoosenInd;
    public int IndexInRoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Activate()
    {
        ChoosenInd = UnityEngine.Random.Range(0, Possible.Count);
        int levelType = LevelController.thisLevelType;
        GameObject g =  Instantiate(Resources.Load<GameObject>("Objects/Cutlery" + (levelType * 10 + ChoosenInd)), transform.position, transform.rotation/*, GameObject.FindGameObjectWithTag("Room").transform*/);
        g.GetComponent<Interactive>().Index = IndexInRoom;
        Destroy(gameObject);
    }

    public void ActivateOld(string spriteName)
    {
        ChoosenInd = Convert.ToInt32(new string(spriteName.Where(x => char.IsDigit(x)).ToArray()));      
        
        GameObject g = Instantiate(Resources.Load<GameObject>("Objects/" + spriteName), transform.position, transform.rotation/*, GameObject.FindGameObjectWithTag("Room").transform*/);
        g.GetComponent<Interactive>().Index = IndexInRoom;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
