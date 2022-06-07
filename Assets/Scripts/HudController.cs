using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHP()
    {
        GameObject[] bars = GameObject.FindGameObjectsWithTag("HealthHUD");
        Player player = FindObjectOfType<Player>();
        List<Common.HealthType> hp = player.Health;

        for (int bar = 0, hlth = 0; bar < bars.Length; bar++)
        {
            string sprite = "";

            if (hlth <= player.Health.Count - 1)
            {
                if (hlth == player.Health.Count - 1)
                    sprite += "Half";

                if (player.Health[hlth] == Common.HealthType.Ketchup)
                    sprite += "Ketchup";

                sprite += "HealthBar";

                hlth += 2;
            }
            else sprite = "EmptyHealthBar";

            bars[bar].GetComponent<Image>().sprite = Resources.Load<Sprite>(sprite);
        }
    }

    public void DrawMiniMap(int[,] rooms, bool[,] visited, Common.Coords playerPos)
    {
        foreach (GameObject mapTile in GameObject.FindGameObjectsWithTag("MapTile"))
            Destroy(mapTile);

        GameObject mmap = GameObject.Find("MiniMap");
        for (int x = 0; x < 5; x++)
            for (int y = 0; y < 5; y++)
            {
                if (rooms[x, y] != 0)
                {
                    Vector3 pos = new Vector3(
                        mmap.transform.position.x - 0.38f * (x - 2),
                        mmap.transform.position.y - 0.38f * (y - 2),
                        0);

                    GameObject Tile = Resources.Load<GameObject>("MapTile");
                    if (visited[x, y])
                    {
                        if (x != playerPos.x || y != playerPos.y)
                            Tile.GetComponent<SpriteRenderer>().color = new Color(0.55f, 0.55f, 0.55f, 0.8f);
                        else
                            Tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);
                    }
                    else
                        Tile.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 0.8f);


                    Instantiate(Tile, pos, mmap.transform.rotation);

                    

                }
            }

    }
}