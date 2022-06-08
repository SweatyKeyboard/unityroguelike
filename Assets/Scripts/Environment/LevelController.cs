using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    bool[,] roomVisited = new bool[5, 5];
    int[,] roomPrototypes = new int[5, 5];
    Common.Coords playerPos = new Common.Coords(2, 2);
    Common.Coords bossPos;

    bool thereAreEnemies = false;
    Entrance[] entrances = new Entrance[4];
    int lastDoor = 0;
    List<MemoryItem>[,] roomItems = new List<MemoryItem>[5, 5];
    List<MemoryInteractive>[,] roomInteractives = new List<MemoryInteractive>[5, 5];
    bool bossDefeated = false;

    void Start()
    {
        NewLevel(); 
    }

    void NewLevel()
    {
        for (int a = 0; a < 5; a++)
            for (int b = 0; b < 5; b++)
            {
                roomItems[a, b] = new List<MemoryItem>();
                roomInteractives[a, b] = new List<MemoryInteractive>();
            }

        roomPrototypes[2, 2] = -1;
        GenerateRooms();
        roomVisited[2, 2] = true;

        Entrance[] tempEntrances = FindObjectsOfType<Entrance>();
        for (int c = 0; c < 4; c++)
            entrances[c] = tempEntrances[c];

        

        Instantiate(Resources.Load<GameObject>("Rooms/Room" + roomPrototypes[playerPos.x, playerPos.y]), transform.position, transform.rotation);
        ActivateDoors();

        FindObjectOfType<HudController>().DrawMiniMap(roomPrototypes, roomVisited, playerPos, bossPos);      
    }

    public void AreEnemiesDead()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            thereAreEnemies = true;
        else
            thereAreEnemies = false;

        if (!thereAreEnemies)
        {
            Vector3 pos = new Vector3(
                transform.position.x + Random.Range(-1f, 1f),
                transform.position.y + Random.Range(-1f, 1f));
            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));

            Instantiate(Resources.Load<GameObject>("Items/Item0"), pos, rot);
            ActivateDoors();
            if (playerPos.x == bossPos.x && playerPos.y == bossPos.y)
            {
                FindObjectOfType<Downfloor>().Open();
                bossDefeated = true;
            }
        }
    }

    public void NewEnemies()
    {
        thereAreEnemies = true;
    }

    IEnumerator SmoothColor(Image rend, Color startColor, Color endColor, float time)
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

    public void WalkToPart1(int x, int y, int i)
    {
        roomItems[playerPos.x, playerPos.y].Clear();
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Item"))
        {
            roomItems[playerPos.x, playerPos.y].Add(
                new MemoryItem(
                p.GetComponent<ItemInfo>().ItemId,
                p.transform.position,
                p.transform.rotation));
            Destroy(p);
        }
        roomInteractives[playerPos.x, playerPos.y].Clear();
        foreach (Interactive p in FindObjectsOfType<Interactive>())
        {
            if (p.GetComponent<Interactive>().Type == Common.InteractiveType.Stove)
                roomInteractives[playerPos.x, playerPos.y].Add(
                    new MemoryInteractive(
                    Common.InteractiveType.Stove,
                    p.GetComponent<Stove>().TurnedOn));
        }

        StartCoroutine(
            SmoothColor(GameObject.FindGameObjectWithTag("Overlay").GetComponent<Image>(),
            new Color(0, 0, 0, 0),
            new Color(0, 0, 0, 1),
            0.3f));

        playerPos.x += x;
        playerPos.y += y;
        lastDoor = i;

        Invoke("WalkToPart2", 0.3f);

    }

    public void WalkToPart2()
    {

        Destroy(GameObject.FindGameObjectWithTag("Room"));

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Particles"))
            Destroy(p);
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("EnemyTrail"))
            Destroy(p);

        StartCoroutine(
            SmoothColor(GameObject.FindGameObjectWithTag("Overlay").GetComponent<Image>(),
            new Color(0, 0, 0, 1),
            new Color(0, 0, 0, 0),
            0.3f));

        Instantiate(Resources.Load<GameObject>("Rooms/Room" + roomPrototypes[playerPos.x, playerPos.y]), transform.position, transform.rotation);
        foreach (MemoryItem g in roomItems[playerPos.x, playerPos.y])
        {
            Instantiate(
                Resources.Load<GameObject>("Items/Item" + g.itemId),
                g.position,
                g.rotation);
        }

        int stoveCounter = 0;
        foreach (MemoryInteractive g in roomInteractives[playerPos.x, playerPos.y])
        {
            if (g.type == Common.InteractiveType.Stove)
            {
                FindObjectsOfType<Stove>()[stoveCounter++].TurnedOn = g.condition;
            }


        }

        if (!roomVisited[playerPos.x, playerPos.y])
        {
            for (int s = 0; s < FindObjectsOfType<EnemySpawn>().Length; s++)
            {
                FindObjectsOfType<EnemySpawn>()[s].Activate();
            }
            for (int s = 0; s < FindObjectsOfType<BoxSpawner>().Length; s++)
            {
                FindObjectsOfType<BoxSpawner>()[s].Activate();
            }
        }
        else
        {
            for (int s = 0; s < FindObjectsOfType<EnemySpawn>().Length; s++)
            {
                FindObjectsOfType<EnemySpawn>()[s].Deactivate();
            }
            for (int s = 0; s < FindObjectsOfType<BoxSpawner>().Length; s++)
            {
                FindObjectsOfType<BoxSpawner>()[s].Deactivate();
            }
        }

        ActivateDoors();

        FindObjectOfType<Player>().transform.position = entrances[lastDoor].transform.position;
        roomVisited[playerPos.x, playerPos.y] = true;

        if (playerPos == bossPos && bossDefeated)
        {
            FindObjectOfType<Downfloor>().Open();
        }


        FindObjectOfType<HudController>().DrawMiniMap(roomPrototypes, roomVisited, playerPos, bossPos);
    }

    void ActivateDoors()
    {
        if (thereAreEnemies)
            for (int c = 0; c < 4; c++)
                entrances[c].Deactivate();
        else
        {
            if (playerPos.x > 0 && roomPrototypes[playerPos.x - 1, playerPos.y] != 0)
                entrances[0].Activate();
            else
                entrances[0].Deactivate();

            if (playerPos.x < 4 && roomPrototypes[playerPos.x + 1, playerPos.y] != 0)
                entrances[3].Activate();
            else
                entrances[3].Deactivate();

            if (playerPos.y > 0 && roomPrototypes[playerPos.x, playerPos.y - 1] != 0)
                entrances[2].Activate();
            else
                entrances[2].Deactivate();

            if (playerPos.y < 4 && roomPrototypes[playerPos.x, playerPos.y + 1] != 0)
                entrances[1].Activate();
            else
                entrances[1].Deactivate();
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

                if (roomCount < maxRooms - 1)
                    roomPrototypes[randomizedPossible.x, randomizedPossible.y] =
                    Random.Range(1, 4);
                else if (roomCount == maxRooms - 1)
                {
                    roomPrototypes[randomizedPossible.x, randomizedPossible.y] = -2;
                    bossPos.x = randomizedPossible.x;
                    bossPos.y = randomizedPossible.y;
                }
                else
                    break;

                roomCount++;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

