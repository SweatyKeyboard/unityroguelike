using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Common.ItemType Type;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController game = FindObjectOfType<GameController>();
            Player player = FindObjectOfType<Player>();
            string message = "";
            int cost = 0;

            switch (Type)
            {
                case Common.ItemType.HealthK:
                    {
                        FindObjectOfType<Player>().AddHP(Common.HealthType.Ketchup);
                        FindObjectOfType<HudController>().UpdateHP();
                        cost = 10;
                    } break;

                case Common.ItemType.HealthMy:
                    {
                        FindObjectOfType<Player>().AddHP(Common.HealthType.Mayo);
                        FindObjectOfType<HudController>().UpdateHP();
                        cost = 12;
                    }
                    break;

                case Common.ItemType.HealthMs:
                    {
                        FindObjectOfType<Player>().AddHP(Common.HealthType.Mustard);
                        FindObjectOfType<HudController>().UpdateHP();
                        cost = 15;
                    }
                    break;

                case Common.ItemType.Salt:
                    {
                        int rand = Random.Range(1, 4);
                        game.Salt += rand;
                        GameObject.Find("SaltCounter").GetComponent<Text>().text = game.Salt.ToString();
                        cost = rand;
                    }
                    break;

                case Common.ItemType.Pepper:
                    {
                        int rand = Random.Range(1, 4);
                        cost = rand * 3;
                    }
                    break;

                case Common.ItemType.Cucumber:
                    {
                        player.Damage += 0.25f;
                        message = "Урон повышен";
                        cost = 25;
                    } break;

                case Common.ItemType.Tomato:
                    {
                        player.Range += 0.05f;
                        message = "Дальность стрельбы повышена";
                        cost = 25;
                    }
                    break;

                case Common.ItemType.Cheese:
                    {
                        player.RateOfFire -= 0.1f;
                        message = "Скорость стрельбы повышена";
                        cost = 25;
                    }
                    break;

                case Common.ItemType.Salad:
                    {
                        player.Speed += 2f;
                        message = "Скорость повышена";
                        cost = 25;
                    } break;

                case Common.ItemType.Bacon:
                    {
                        player.BulletSpeed += 0.08f;
                        message = "Скорость пуль повышена";
                        cost = 25;
                    } break;

                case Common.ItemType.Burger:
                    {
                        player.MaxHealth += 2;
                        message = "Максимальное здоровье повышено";
                        FindObjectOfType<HPPos>().GetUpdates();
                        cost = 40;
                    }
                    break;
            }
            FindObjectOfType<InfoText>().ShowText(message, 2f);
            FindObjectOfType<GameController>().Score -= cost;
            FindObjectOfType<GameController>().BonusesCollected++;

            Destroy(gameObject);
        }
    }
}
