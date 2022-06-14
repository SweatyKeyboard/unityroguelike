using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetaShopItem : MonoBehaviour
{
    [SerializeField] int startPrice;
    [SerializeField] string item;
    [SerializeField] float priceModifier;
    [SerializeField] string text;

    [SerializeField] Text textUI;
    [SerializeField] Text priceUI;
    [SerializeField] Text pepperUI;
    int totalPrice;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        if (PlayerPrefs.HasKey(item))
            i = PlayerPrefs.GetInt(item);

        totalPrice = startPrice;
        for (int c = 0; c < i; c++)
            totalPrice = (int)(totalPrice * priceModifier);

        priceUI.text = totalPrice.ToString();
        textUI.text = text + ": " + i;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Buy()
    {
        int pepper = 0;
        if (PlayerPrefs.HasKey("Pepper"))
            pepper = PlayerPrefs.GetInt("Pepper");

        if (pepper >= totalPrice)
        {
            pepper -= totalPrice;
            PlayerPrefs.SetInt("Pepper", pepper);
            pepperUI.text = pepper.ToString();

            int i = PlayerPrefs.GetInt(item);
            PlayerPrefs.SetInt(item, ++i);
            textUI.text = text + ": " + (i);

            totalPrice = startPrice;
            for (int c = 0; c < i; c++)
                totalPrice = (int)(totalPrice * priceModifier);
            priceUI.text = totalPrice.ToString();
        }
    }
}
