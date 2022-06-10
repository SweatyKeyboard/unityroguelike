using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public bool IsForSale = false;
    public int Price;
    public bool Bought = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && IsForSale)
        {
            FindObjectOfType<InfoText>().ShowText("Цена: " + Price + " ед. соли");
        }

        ButtonClick b = FindObjectOfType<ButtonClick>();
        if (collision.CompareTag("Player") && IsForSale && !Bought &&
            ((Application.isMobilePlatform && b.IsCliked && b.Key == KeyCode.Space) ||
            (!Application.isMobilePlatform) && Input.GetKey(KeyCode.Space)))
        {
            if (GameController.Salt >= Price)
            {
                Bought = true;
                GameController.Salt -= Price;
                GameObject.Find("SaltCounter").GetComponent<Text>().text = GameController.Salt.ToString();
            }
            else
            {
                FindObjectOfType<InfoText>().ShowText("Недостаточно соли",1.5f, new Color(0.9f,0.3f,0.3f));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<InfoText>().HideText();
        }
    }
}
