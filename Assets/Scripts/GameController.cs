using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Common.Coords playerPos = new Common.Coords(2, 2);
    Player player;
    bool thereAreEnemies = false;
    Entrance[] entrances = new Entrance[4];
    int lastDoor = 0;
    HudController hud;

    int[,] roomPrototypes = new int[5, 5];
    bool[,] roomVisited = new bool[5, 5];
    List<Memory>[,] roomItems = new List<Memory>[5, 5];

    void Start()
    {
        for (int a = 0; a < 5; a++)
            for (int b = 0; b < 5; b++)
            {
                roomItems[a, b] = new List<Memory>();
            }

        roomPrototypes[2, 2] = -1;
        GenerateRooms();
    
        roomVisited[2, 2] = true;
        //roomPrototypes[2, 1] = 1; 

        Entrance[] tempEntrances = FindObjectsOfType<Entrance>();
        for (int c = 0; c < 4; c++)
            entrances[c] = tempEntrances[c];

        player = FindObjectOfType<Player>();
        for (int c = 0; c < 6; c++)
        {
           player.AddHP(Common.HealthType.Ketchup);
        }

        hud = FindObjectOfType<HudController>();
        hud.UpdateHP();
        hud.DrawMiniMap(roomPrototypes, playerPos);
        
        Instantiate(Resources.Load<GameObject>("Rooms/Room" + roomPrototypes[playerPos.x, playerPos.y]), transform.position, transform.rotation);
        ActivateDoors();
    }

    public void AreEnemiesDead()
    {      
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            thereAreEnemies = true;
        else
            thereAreEnemies = false;

        if (!thereAreEnemies)
        {
            Instantiate(Resources.Load<GameObject>("Items/Item0"), transform.position, transform.rotation);
            ActivateDoors();
        }
    }

    public void NewEnemies()
    {
        thereAreEnemies = true;
    }

    public void WalkToStart(int x, int y, int i)
    {
        Destroy(GameObject.FindGameObjectWithTag("Room"));

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Particles"))
            Destroy(p);

        roomItems[playerPos.x, playerPos.y].Clear();
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Item"))
        {
            roomItems[playerPos.x, playerPos.y].Add(
                new Memory(
                p.GetComponent<ItemInfo>().ItemId,
                p.transform.position,
                p.transform.rotation));
            Destroy(p);
        }

        StartCoroutine(
            SmoothColor(GameObject.FindGameObjectWithTag("Overlay").GetComponent<Image>(),
            new Color(0,0,0,0),
            new Color(0,0,0,1),
            0.3f));

        playerPos.x += x;
        playerPos.y += y;
        lastDoor = i;

        Invoke("WalkToEnd", 0.3f);
        
    }

    public void WalkToEnd()
    {
        StartCoroutine(
            SmoothColor(GameObject.FindGameObjectWithTag("Overlay").GetComponent<Image>(),
            new Color(0, 0, 0, 1),
            new Color(0, 0, 0, 0),
            0.3f));

        Instantiate(Resources.Load<GameObject>("Rooms/Room" + roomPrototypes[playerPos.x, playerPos.y]), transform.position, transform.rotation);
        foreach (Memory g in roomItems[playerPos.x, playerPos.y])
        {
            Instantiate(
                Resources.Load<GameObject>("Items/Item" + g.itemId),
                g.position,
                g.rotation);
        }
        
        if (!roomVisited[playerPos.x, playerPos.y])
        {
            for (int s = 0; s < FindObjectsOfType<EnemySpawn>().Length; s++)
            {
                FindObjectsOfType<EnemySpawn>()[s].Activete();
            }
        }
        else
        {
            for (int s = 0; s < FindObjectsOfType<EnemySpawn>().Length; s++)
            {
                FindObjectsOfType<EnemySpawn>()[s].Deactivate();
            }
        }

        ActivateDoors();

        player.transform.position = entrances[lastDoor].transform.position;
        roomVisited[playerPos.x, playerPos.y] = true;
        hud.DrawMiniMap(roomPrototypes, playerPos);
    }
    IEnumerator SmoothColor(Image rend, Color startColor, Color endColor, float time )
        {
            float currTime = 0f;
            rend.color = startColor;
            do
            {
                rend.color = Color.Lerp(rend.color, endColor, currTime / time);
                currTime += Time.deltaTime;
                yield return null;
            } while (currTime <= time);
        }

    void Update()
    {
        
    }

    void ActivateDoors()
    {
        if (thereAreEnemies)
            for (int c = 0; c < 4; c++)
                entrances[c].Deactivate();
        else
        {
            if (playerPos.x > 0 && roomPrototypes[playerPos.x - 1, playerPos.y] != 0)
                entrances[1].Activate();
            else
                entrances[1].Deactivate();

            if (playerPos.x < 4 && roomPrototypes[playerPos.x + 1, playerPos.y] != 0)
                entrances[2].Activate();
            else
                entrances[2].Deactivate();

            if (playerPos.y > 0 && roomPrototypes[playerPos.x, playerPos.y - 1] != 0)
                entrances[0].Activate();
            else
                entrances[0].Deactivate();

            if (playerPos.y < 4 && roomPrototypes[playerPos.x, playerPos.y + 1] != 0)
                entrances[3].Activate();
            else
                entrances[3].Deactivate();
        }
    }

    void GenerateRooms()
    {
        int maxRooms = (5 - 1) * (5 - 1) - Random.Range(0, 5 + 1);
        int roomCount = 1;

        while (roomCount < maxRooms)
        {
            List<Common.Coords> possible = new List<Common.Coords>();

            for (int x = 0; x < 5; x++)
                for (int y = 0; y < 5; y++)
                {
                    if (roomPrototypes[x, y] != 0)
                    {
                        if (x != 0 && roomPrototypes[x - 1, y] == 0)
                            possible.Add(new Common.Coords(x - 1, y));
                        if (x != 4 && roomPrototypes[x + 1, y] == 0)
                            possible.Add(new Common.Coords(x + 1, y));
                        if (y != 0 && roomPrototypes[x, y - 1] == 0)
                            possible.Add(new Common.Coords(x, y - 1));
                        if (y != 4 && roomPrototypes[x, y + 1] == 0)
                            possible.Add(new Common.Coords(x, y + 1));
                    }
                }

            for (int c = 0; c < 2; c++)
            {
                Common.Coords randomizedPossible = possible[Random.Range(0, possible.Count)];
                roomPrototypes[randomizedPossible.x, randomizedPossible.y] = 1;
                roomCount++;
                if (roomCount == maxRooms)
                    break;
            }
        }

    }
}
