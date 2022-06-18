using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    [SerializeField] Common.ItemType type;
    [SerializeField] AudioClip sound;

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

                if (!GetComponent<ShopItem>().IsForSale)
                    AudioSource.PlayClipAtPoint(sound, new Vector3(0,0,-10));

                switch (type)
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

                    case Common.ItemType.HealthBbq:
                        {
                            FindObjectOfType<Player>().AddHP(Common.HealthType.Bbq);
                            FindObjectOfType<HudController>().UpdateHP();
                            cost = 15;
                        } break;

                    case Common.ItemType.HealthChl:
                        {
                            FindObjectOfType<Player>().AddHP(Common.HealthType.Chili);
                            FindObjectOfType<HudController>().UpdateHP();
                            cost = 15;
                        }
                        break;

                    case Common.ItemType.HealthChs:
                        {
                            FindObjectOfType<Player>().AddHP(Common.HealthType.Cheese);
                            FindObjectOfType<HudController>().UpdateHP();
                            cost = 15;
                        }
                        break;

                    case Common.ItemType.HealthGar:
                        {
                            FindObjectOfType<Player>().AddHP(Common.HealthType.Garlic);
                            FindObjectOfType<HudController>().UpdateHP();
                            cost = 15;
                        }
                        break;

                    case Common.ItemType.HealthSoy:
                        {
                            FindObjectOfType<Player>().AddHP(Common.HealthType.Soy);
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
                            message = "Damage up";
                            cost = 25;
                        }
                        break;

                    case Common.ItemType.Tomato:
                        {
                            player.RangeBar++;
                            player.UpdateCharacteristics();
                            message = "Bullet lifetime up";
                            cost = 25;
                        }
                        break;

                    case Common.ItemType.Cheese:
                        {
                            player.RateOfFireBar++;
                            player.UpdateCharacteristics();
                            message = "Rate oà fire up";
                            cost = 25;
                        }
                        break;

                    case Common.ItemType.Salad:
                        {
                            player.SpeedBar++;
                            player.UpdateCharacteristics();
                            message = "Speed up";
                            cost = 25;
                        }
                        break;

                    case Common.ItemType.Bacon:
                        {
                            player.BulletSpeedBar++;
                            player.UpdateCharacteristics();
                            message = "Bullet speed up";
                            cost = 25;
                        }
                        break;

                    case Common.ItemType.Burger:
                        {
                            player.MaxHealth += 2;
                            message = "Max HP up";
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
