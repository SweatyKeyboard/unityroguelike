using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int Salt;
    public bool Pause = false;
    /*public*/ GameObject PauseMenu;

    Common.Coords playerPos = new Common.Coords(2, 2);
    Player player;
    bool thereAreEnemies = false;
    Entrance[] entrances = new Entrance[4];
    int lastDoor = 0;
    HudController hud;
    int[,] roomPrototypes = new int[5, 5];
    bool[,] roomVisited = new bool[5, 5];
    List<MemoryItem>[,] roomItems = new List<MemoryItem>[5, 5];
    List<MemoryInteractive>[,] roomInteractives = new List<MemoryInteractive>[5, 5];
    bool pauseClicked = false;

    void Update()
    {
        if (!pauseClicked && Input.GetKey(KeyCode.Escape))
        {
            pauseClicked = true;
            PauseGame();
        }
    }

    void Start()
    {
        PauseMenu = GameObject.Find("PauseCanvas");
        //Instantiate(PauseMenu, transform.position, transform.rotation);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;

        Screen.SetResolution(1400, 1050, true);
        for (int a = 0; a < 5; a++)
            for (int b = 0; b < 5; b++)
            {
                roomItems[a, b] = new List<MemoryItem>();
                roomInteractives[a, b] = new List<MemoryInteractive>();
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
        
        Instantiate(Resources.Load<GameObject>("Rooms/Room" + roomPrototypes[playerPos.x, playerPos.y]), transform.position, transform.rotation);
        ActivateDoors();

        hud = FindObjectOfType<HudController>();
        hud.UpdateHP();
        hud.DrawMiniMap(roomPrototypes, roomVisited, playerPos);
    }

    public void PauseGame()
    {
        if (Pause == true)
        {
            
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        Pause = !Pause;
        Invoke("PauseIsCliked", 0.5f);
    }

    void PauseIsCliked()
    {
        pauseClicked = false;
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

        hud.DrawMiniMap(roomPrototypes, roomVisited, playerPos);
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

    void ActivateDoors()
    {
        if (thereAreEnemies)
            for (int c = 0; c < 4; c++)
                entrances[c].Deactivate();
        else
        {
            if (playerPos.x > 0 && roomPrototypes[playerPos.x - 1, playerPos.y] != 0)
                entrances[2].Activate();
            else
                entrances[2].Deactivate();

            if (playerPos.x < 4 && roomPrototypes[playerPos.x + 1, playerPos.y] != 0)
                entrances[1].Activate();
            else
                entrances[1].Deactivate();

            if (playerPos.y > 0 && roomPrototypes[playerPos.x, playerPos.y - 1] != 0)
                entrances[3].Activate();
            else
                entrances[3].Deactivate();

            if (playerPos.y < 4 && roomPrototypes[playerPos.x, playerPos.y + 1] != 0)
                entrances[0].Activate();
            else
                entrances[0].Deactivate();
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
                roomPrototypes[randomizedPossible.x, randomizedPossible.y] = 
                    Random.Range(1,4);
                roomCount++;
                if (roomCount == maxRooms)
                    break;
            }
        }

    }
}
