using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetaShopItem : MonoBehaviour
{
    public int StartPrice;
    public string Item;
    public float PriceModifier;

    public Text PriceUI;
    int totalPrice;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        if (PlayerPrefs.HasKey(Item))
            i = PlayerPrefs.GetInt(Item);


        for (int c = 0; c < i; c++)
            totalPrice = (int)(totalPrice * PriceModifier);

        PriceUI.text = StartPrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Buy()
    {
        int pepper = PlayerPrefs.GetInt("Pepper");
        if (pepper >= totalPrice)
        {
            pepper -= totalPrice;
            PlayerPrefs.SetInt("Pepper", pepper);
            int i = PlayerPrefs.GetInt(Item);
            PlayerPrefs.SetInt(Item, i+1);

        }
    }
}
