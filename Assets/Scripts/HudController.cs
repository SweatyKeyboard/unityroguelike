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
        List<Common.HealthType> hp = GameObject.FindObjectOfType<Player>().Health;
        GameObject[] bars = GameObject.FindGameObjectsWithTag("HealthHUD");
        Player player = FindObjectOfType<Player>();

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
}
