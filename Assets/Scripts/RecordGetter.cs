using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordGetter : MonoBehaviour
{
    void Start()
    {
        UpdateList();
    }

    public void UpdateList()
    {
        List<string> names = new List<string>();
        List<int> scores = new List<int>();

        if (PlayerPrefs.HasKey("RecordsCount"))
        {
            int count = PlayerPrefs.GetInt("RecordsCount");
            for (int c = 0; c < count; c++)
            {
                names.Add(PlayerPrefs.GetString("Name" + c));
                scores.Add(PlayerPrefs.GetInt("Score" + c));
            }

            for (int z = 0; z < count; z++)
            for (int y = 0; y < count - 1; y++)
            {
                for (int x = y; x < count - 1; x++)
                {
                    if (scores[x] < scores[x + 1])
                    {
                        string tempS = names[x];
                        names[x] = names[x + 1];
                        names[x + 1] = tempS;

                        int tempI = scores[x];
                        scores[x] = scores[x + 1];
                        scores[x + 1] = tempI;
                    }
                }
            }

            string outText = "";
            for (int c = 0; c < count; c++)
            {
                outText += (c + 1) + ".  " + names[c] + "  -  " + scores[c] + "\n";
            }
            GetComponent<Text>().text = outText;
        }
        else
        {
            GetComponent<Text>().text = "There are no records";
        }
    }

    void Update()
    {
        
    }
}
