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
            switch (Type)
            {
                case Common.ItemType.HealthK:
                    {
                        FindObjectOfType<Player>().AddHP(Common.HealthType.Ketchup);
                        FindObjectOfType<HudController>().UpdateHP();
                    } break;

                case Common.ItemType.HealthMy:
                    {
                        FindObjectOfType<Player>().AddHP(Common.HealthType.Mayo);
                        FindObjectOfType<HudController>().UpdateHP();
                    }
                    break;

                case Common.ItemType.HealthMs:
                    {
                        FindObjectOfType<Player>().AddHP(Common.HealthType.Mustard);
                        FindObjectOfType<HudController>().UpdateHP();
                    }
                    break;

                case Common.ItemType.Salt:
                    {
                        game.Salt += Random.Range(1, 4);
                        GameObject.Find("SaltCounter").GetComponent<Text>().text = game.Salt.ToString();
                    }
                    break;

                case Common.ItemType.Pepper:
                    {
                    }
                    break;

                case Common.ItemType.Cucumber:
                    {
                        player.Damage += 0.25f;
                        message = "Урон повышен";
                    } break;

                case Common.ItemType.Tomato:
                    {
                        player.Range += 0.05f;
                        message = "Дальность стрельбы повышена";
                    }
                    break;

                case Common.ItemType.Cheese:
                    {
                        player.RateOfFire -= 0.1f;
                        message = "Скорость стрельбы повышена";
                    }break;

                case Common.ItemType.Salad:
                    {
                        player.Speed += 0.5f;
                        message = "Скорость повышена";
                    } break;

                case Common.ItemType.Bacon:
                    {
                        player.BulletSpeed += 0.08f;
                        message = "Скорость пуль повышена";
                    } break;

                case Common.ItemType.Burger:
                    {
                        player.MaxHealth += 2;
                        message = "Максимальное здоровье повышено";
                        FindObjectOfType<HPPos>().GetUpdates();
                    }
                    break;
            }
            FindObjectOfType<InfoText>().ShowText(message, 2f);


            Destroy(gameObject);
        }
    }
}
