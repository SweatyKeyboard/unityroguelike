using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{

    public void UpdateHP()
    {
        GameObject[] bars = GameObject.FindGameObjectsWithTag("HealthHUD");
        Player player = FindObjectOfType<Player>();
        int[] hp = player.Health;


        for (int bar = 0, hlth = 0; bar < player.MaxHealth / 2; bar++)
        {
            string sprite = "";
            if (hlth <= hp[0] - 1)
            {
                if (hlth == hp[0] - 1)
                    sprite += "Half";
                sprite += "KetchupHealthBar";

                hlth += 2;
            }
            else sprite = "EmptyHealthBar";

            bars[bar].GetComponent<Image>().sprite = Resources.Load<Sprite>(sprite);
        }

        int additional = 0;
        for (int i = 1; i < hp.Length; i++)
        {
            int typedBars = (int)Math.Ceiling(hp[i] / 2.0);

            for (int j = player.MaxHealth / 2; j < bars.Length; j++)
                Destroy(bars[j]);

            for (int bar = 0, hlth = 0; bar < typedBars; bar++)
            {
                string sprite = "";
                switch (i)
                {
                    case 1: sprite = "Mayo";  break;
                    case 2: sprite = "Cheese"; break;
                    case 3: sprite = "Soy"; break;
                    case 4: sprite = "Bbq"; break;
                    case 5: sprite = "Garlic"; break;
                    case 6: sprite = "Mustard"; break;
                    case 7: sprite = "Chilli"; break;
                }
                if (hlth <= hp[i] - 1)
                {
                    if (hlth == hp[i] - 1)
                        sprite = "Half" + sprite;
                    sprite += "HealthBar";

                    hlth += 2;
                    FindObjectOfType<HPPos>().AddHeart(player.MaxHealth / 2 + additional++, sprite);
                }


            }
        }

    }

    public void DrawMiniMap(Map map, Common.Coords playerPos)
    {

        foreach (GameObject mapTile in GameObject.FindGameObjectsWithTag("MapTile"))
            Destroy(mapTile);

        int lvlSize = FindObjectOfType<LevelController>().levelSize;
        GameObject mmap = GameObject.Find("MiniMap");

        float sizeMod = 1.5f / lvlSize;
        float bias = 1.9f / lvlSize;
        for (int x = 0; x < lvlSize; x++)
            for (int y = 0; y < lvlSize; y++)
            {
                if (map.Prototypes[x, y] != 0)
                {
                    if (map.Known[x, y])
                    {
                        Vector3 position = new Vector3(
                            mmap.transform.position.x - bias * (x - lvlSize / 2),
                            mmap.transform.position.y - bias * (y - lvlSize / 2),
                            0);
                        GameObject tile = Instantiate(Resources.Load<GameObject>("MapTile"), position, mmap.transform.rotation);
                        if (map.Visited[x, y])
                        {
                            if (x == playerPos.x && y == playerPos.y)
                                tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);
                            else
                                tile.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f, 0.8f);

                        }
                        else
                        {
                                tile.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
                        }

                        if (x == map.bossCoords.x && y == map.bossCoords.y)
                            tile.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.2f, 0.2f, 0.8f);
                        else
                                if (x == map.shopCoords.x && y == map.shopCoords.y)
                            tile.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.45f, 0.2f, 0.8f);

                        tile.transform.localScale = new Vector3(
                            sizeMod, sizeMod, 0
                            );
                    }
                }
            }

    }
}
