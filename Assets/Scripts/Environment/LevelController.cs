using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelController : MonoBehaviour
{
    public int levelSize;
    public int center;

    int thisLevelType = 0;
    bool[,] roomVisited;
    int[,] roomPrototypes;
    Common.Coords playerPos = new Common.Coords(2, 2);
    Common.Coords bossPos;

    bool thereAreEnemies = false;
    Entrance[] entrances = new Entrance[4];
    int lastDoor = 0;
    List<MemoryItem>[,] roomItems;
    List<MemoryInteractive>[,] roomInteractives;
    bool bossDefeated = false;

    void Start()
    {
        NewLevelPart1();
    }

    public void NewLevelPart1()
    {
        levelSize++;
        center = levelSize / 2;
        bossDefeated = false;
        thisLevelType = UnityEngine.Random.Range(0, 1);
        FindObjectOfType<GameController>().FloorsCompleted++;

        Image overlay = GameObject.FindGameObjectWithTag("Overlay").GetComponent<Image>();

        StartCoroutine(
            SmoothColor(overlay,
            new Color(0, 0, 0, overlay.color.a),
            new Color(0, 0, 0, 1),
            0.5f));
        Invoke("NewLevelPart2", 0.5f);

    }

    void NewLevelPart2()
    {
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
            Destroy(room);        
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Particles"))
            Destroy(p);
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("EnemyTrail"))
            Destroy(p);
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("PlayerTrail"))
            Destroy(p);

        playerPos = new Common.Coords(center, center);
        
        GenerateRooms();
        roomVisited[center, center] = true;
        Entrance[] tempEntrances = FindObjectsOfType<Entrance>();
        for (int c = 0; c < 4; c++)
            entrances[c] = tempEntrances[c];



        Instantiate(Resources.Load<GameObject>("Rooms/Room" + roomPrototypes[playerPos.x, playerPos.y]), transform.position, transform.rotation);
        ActivateDoors();

        FindObjectOfType<HudController>().DrawMiniMap(roomPrototypes, roomVisited, playerPos, bossPos);


        StartCoroutine(
           SmoothColor(GameObject.FindGameObjectWithTag("Overlay").GetComponent<Image>(),
           new Color(0, 0, 0, 1),
           new Color(0, 0, 0, 0),
           0.5f));

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
                transform.position.x + UnityEngine.Random.Range(-1f, 1f),
                transform.position.y + UnityEngine.Random.Range(-1f, 1f));
            Quaternion rot = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));

            Instantiate(Resources.Load<GameObject>("Items/Item0"), pos, rot);
            ActivateDoors();
            if (playerPos.x == bossPos.x && playerPos.y == bossPos.y)
            {
                FindObjectOfType<Downfloor>().Open();
                bossDefeated = true;
            }

            FindObjectOfType<GameController>().RoomsCleared++;
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
            else if (p.GetComponent<Interactive>().Type == Common.InteractiveType.Cutlery)
                roomInteractives[playerPos.x, playerPos.y].Add(
                    new MemoryInteractive(
                    Common.InteractiveType.Cutlery,
                    p.GetComponent<SpriteRenderer>().sprite.name));
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
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("PlayerTrail"))
            Destroy(p);

        Interactive[] arr = FindObjectsOfType<Interactive>();
        for (int c = 0; c < arr.Length; c++)
        {
            Destroy(arr[c].gameObject);
        }

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
        int cutleryCounter = 0;
        foreach (MemoryInteractive g in roomInteractives[playerPos.x, playerPos.y])
        {
            if (g.type == Common.InteractiveType.Stove)
            {
                FindObjectsOfType<Stove>()[stoveCounter++].TurnedOn = g.boolean;
            }
            else
            {
                if (g.type == Common.InteractiveType.Cutlery)
                {
                    if(roomVisited[playerPos.x, playerPos.y])
                        FindObjectsOfType<CutlerySpawner>()[cutleryCounter++].ActivateOld(g.integer);
                }
            }
        }

        if (!roomVisited[playerPos.x, playerPos.y])
        {
            for (int s = 0; s < FindObjectsOfType<EnemySpawn>().Length; s++)
                FindObjectsOfType<EnemySpawn>()[s].Activate();

            for (int s = 0; s < FindObjectsOfType<BoxSpawner>().Length; s++)
                FindObjectsOfType<BoxSpawner>()[s].Activate();

            for (int s = 0; s < FindObjectsOfType<CutlerySpawner>().Length; s++)
                FindObjectsOfType<CutlerySpawner>()[s].Activate();

            FindObjectOfType<GameController>().Score =
                (int)(FindObjectOfType<GameController>().Score * 1.05);
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

            if (playerPos.x < levelSize - 1 && roomPrototypes[playerPos.x + 1, playerPos.y] != 0)
                entrances[3].Activate();
            else
                entrances[3].Deactivate();

            if (playerPos.y > 0 && roomPrototypes[playerPos.x, playerPos.y - 1] != 0)
                entrances[2].Activate();
            else
                entrances[2].Deactivate();

            if (playerPos.y < levelSize - 1 && roomPrototypes[playerPos.x, playerPos.y + 1] != 0)
                entrances[1].Activate();
            else
                entrances[1].Deactivate();
        }
    }

    void GenerateRooms()
    {
        int maxRooms = (levelSize) * (levelSize) - UnityEngine.Random.Range(0, levelSize);
        int roomCount = 1;

        do
        {
            roomVisited = new bool[levelSize, levelSize];
            roomPrototypes = new int[levelSize, levelSize];
            roomItems = new List<MemoryItem>[levelSize, levelSize];
            roomInteractives = new List<MemoryInteractive>[levelSize, levelSize];
            for (int a = 0; a < levelSize; a++)
                for (int b = 0; b < levelSize; b++)
                {
                    roomItems[a, b] = new List<MemoryItem>();
                    roomInteractives[a, b] = new List<MemoryInteractive>();
                    roomVisited[a, b] = false;
                    if (a == 2 && b == 2)
                        roomPrototypes[a, b] = -1;
                    else
                        roomPrototypes[a, b] = 0;
                }
            roomPrototypes[center, center] = -1;

            while (roomCount < maxRooms)
            {
                List<Common.Coords> possible = new List<Common.Coords>();

                for (int x = 0; x < levelSize; x++)
                    for (int y = 0; y < levelSize; y++)
                    {
                        if (roomPrototypes[x, y] != 0)
                        {
                            int thisNeighbors = 0;
                            for (int xxx = -1; xxx <= 1; xxx++)
                                for (int yyy = -1; yyy <= 1; yyy++)
                                {
                                    try
                                    {
                                        if (roomPrototypes[x + xxx, y + yyy] != 0)
                                            thisNeighbors++;
                                    }
                                    catch { }
                                }
                            if (thisNeighbors <= 3)
                            {
                                for (int xx = -1; xx <= 1; xx++)
                                    for (int yy = -1; yy <= 1; yy++)
                                    {

                                        if (Math.Abs(xx) != Math.Abs(yy))
                                        {
                                            try
                                            {

                                                if (roomPrototypes[x + xx, y + yy] == 0)
                                                {
                                                    int neighbors = 0;
                                                    for (int xxx = -1; xxx <= 1; xxx++)
                                                        for (int yyy = -1; yyy <= 1; yyy++)
                                                        {
                                                            try
                                                            {
                                                                if (roomPrototypes[x + xx + xxx, y + yy + yyy] != 0)
                                                                    neighbors++;
                                                            }
                                                            catch { }
                                                        }
                                                    if (neighbors <= 3)
                                                    {
                                                        possible.Add(new Common.Coords(x + xx, y + yy));
                                                    }

                                                }

                                            }

                                            catch { }
                                        }

                                        /*if (x != 0 && roomPrototypes[x - 1, y] == 0)
                                            possible.Add(new Common.Coords(x - 1, y));
                                        if (x != 4 && roomPrototypes[x + 1, y] == 0)
                                            possible.Add(new Common.Coords(x + 1, y));
                                        if (y != 0 && roomPrototypes[x, y - 1] == 0)
                                            possible.Add(new Common.Coords(x, y - 1));
                                        if (y != 4 && roomPrototypes[x, y + 1] == 0)
                                            possible.Add(new Common.Coords(x, y + 1));*/
                                    }
                            }
                        }

                    }
                if (possible.Count > 0)
                {
                    Common.Coords randomizedPossible = possible[UnityEngine.Random.Range(0, possible.Count)];

                    if (roomCount < maxRooms - 1)
                        roomPrototypes[randomizedPossible.x, randomizedPossible.y] =
                        thisLevelType * 10 + UnityEngine.Random.Range(1, 4);
                    roomCount++;
                }
                else
                    break;
            }

            Common.Coords farthestRoom = new Common.Coords(center, center);
            float dist = 0;

            for (int i = 0; i < levelSize; i++)
                for (int j = 0; j < levelSize; j++)
                {
                    if (roomPrototypes[i, j] != 0)
                    {
                        float tmpDist = (float)Math.Sqrt(Math.Pow(Math.Abs(i - center), 2) + Math.Pow(Math.Abs(j - center), 2));
                        if (tmpDist > dist)
                        {
                            farthestRoom = new Common.Coords(i, j);
                            dist = tmpDist;
                        }
                        else if (tmpDist == dist)
                        {
                            if (UnityEngine.Random.Range(0, 2) == 1)
                                farthestRoom = new Common.Coords(i, j);
                        }
                    }
                }

            roomPrototypes[farthestRoom.x, farthestRoom.y] = -2;
            bossPos.x = farthestRoom.x;
            bossPos.y = farthestRoom.y;

        } while (roomCount <= 6);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }


