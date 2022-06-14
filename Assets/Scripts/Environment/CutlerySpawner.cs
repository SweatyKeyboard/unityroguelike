using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CutlerySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> possible;
    [SerializeField] int indexInRoom;
    int ChoosenInd;

    public int IndexInRoom { get { return indexInRoom; } }

    public void Activate()
    {
        ChoosenInd = UnityEngine.Random.Range(0, possible.Count);
        int levelType = LevelController.thisLevelType;
        GameObject g =  Instantiate(Resources.Load<GameObject>("Objects/Cutlery" + (levelType * 10 + ChoosenInd)), transform.position, transform.rotation/*, GameObject.FindGameObjectWithTag("Room").transform*/);
        g.GetComponent<Interactive>().Index = indexInRoom;
        Destroy(gameObject);
    }

    public void ActivateOld(string spriteName)
    {
        ChoosenInd = Convert.ToInt32(new string(spriteName.Where(x => char.IsDigit(x)).ToArray()));      
        
        GameObject g = Instantiate(Resources.Load<GameObject>("Objects/" + spriteName), transform.position, transform.rotation/*, GameObject.FindGameObjectWithTag("Room").transform*/);
        g.GetComponent<Interactive>().Index = indexInRoom;
        Destroy(gameObject);
    }
}
