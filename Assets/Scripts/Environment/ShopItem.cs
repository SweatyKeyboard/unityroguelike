using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] bool isForSale = false;
    [SerializeField] int price;
    [SerializeField] bool bought = false;
    [SerializeField] AudioClip sound;

    public bool IsForSale
    {
        get { return isForSale; }
        set { isForSale = value; }
    }

    public int Price
    {
        get { return price; }
    }

    public bool Bought
    {
        get { return bought; }
    }

    bool keyBlocker = false;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isForSale)
        {
            FindObjectOfType<InfoText>().ShowText("Price: " + price + " salt");
        }

        ButtonClick b = FindObjectOfType<ButtonClick>();
        if (!keyBlocker && collision.CompareTag("Player") && isForSale && !bought &&
            ((Application.isMobilePlatform && b.IsCliked && b.Key == KeyCode.Space) ||
            (!Application.isMobilePlatform) && Input.GetKey(KeyCode.Space)))
        {
            keyBlocker = true;
            Invoke("UnlockKey", 0.5f);
            if (GameController.Salt >= price)
            {
                bought = true;
                GameController.Salt -= price;
                GameObject.Find("SaltCounter").GetComponent<Text>().text = GameController.Salt.ToString();
                AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
            }
            else
            {
                FindObjectOfType<InfoText>().ShowText("Not enough salt",1.5f, new Color(0.9f,0.3f,0.3f));
            }
        }
    }

    void UnlockKey()
    {
        keyBlocker = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isForSale)
        {
            FindObjectOfType<InfoText>().HideText();
        }
    }
}
