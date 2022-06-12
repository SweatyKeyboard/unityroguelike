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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GetComponent<ShopItem>().IsForSale && GetComponent<ShopItem>().Bought
                || !GetComponent<ShopItem>().IsForSale)
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
                        }
                        break;

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
                            GameController.Salt += rand;
                            GameObject.Find("SaltCounter").GetComponent<Text>().text = GameController.Salt.ToString();
                            cost = rand;
                        }
                        break;

                    case Common.ItemType.Pepper:
                        {

                            int pepper = (game.currentLevel);
                            cost = 3 * pepper;
                            if (PlayerPrefs.HasKey("Pepper"))
                                pepper += PlayerPrefs.GetInt("Pepper");

                            PlayerPrefs.SetInt("Pepper", pepper);
                            
                        }
                        break;

                    case Common.ItemType.Cucumber:
                        {
                            player.DamageBar++;
                            player.UpdateCharacteristics();
                            message = "Урон повышен";
                            cost = 25;
                        }
                        break;

                    case Common.ItemType.Tomato:
                        {
                            player.RangeBar++;
                            player.UpdateCharacteristics();
                            message = "Дальность стрельбы повышена";
                            cost = 25;
                        }
                        break;

                    case Common.ItemType.Cheese:
                        {
                            player.RateOfFireBar++;
                            player.UpdateCharacteristics();
                            message = "Скорость стрельбы повышена";
                            cost = 25;
                        }
                        break;

                    case Common.ItemType.Salad:
                        {
                            player.SpeedBar++;
                            player.UpdateCharacteristics();
                            message = "Скорость повышена";
                            cost = 25;
                        }
                        break;

                    case Common.ItemType.Bacon:
                        {
                            player.BulletSpeedBar++;
                            player.UpdateCharacteristics();
                            message = "Скорость пуль повышена";
                            cost = 25;
                        }
                        break;

                    case Common.ItemType.Burger:
                        {
                            player.MaxHealth += 2;
                            message = "Максимальное здоровье повышено";
                            FindObjectOfType<HPPos>().GetUpdates();
                            cost = 40;
                        }
                        break;
                }
                if (message != "")
                    FindObjectOfType<InfoText>().ShowText(message, 2f);
                GameController.Score -= cost;
                GameController.BonusesCollected++;

                Destroy(gameObject);
            }
        }
    }
}
