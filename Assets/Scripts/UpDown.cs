using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UpDown : MonoBehaviour
{

    public Sprite Icon;
    public string Name;
    public int Points;
    public PointsController Controller;
    public int Cost;
    public int IndexInfo;

    public Text NameUI;
    public Image IconUI;
    public Text InfoUI;
    public Text PointsUI;

    // Start is called before the first frame update
    void Start()
    {
        IconUI.sprite = Icon;
        NameUI.text = Name;
        ForceUpdate();
    }

    public void ForceUpdate()
    {
        PointsUI.text = Points.ToString();

        switch(IndexInfo)
        {
            case 0:
                {
                    InfoUI.text = Math.Round(0.45f + (Math.Pow(Points, 0.9) / 7.94), 2) + " sec";
                } break;
            case 1:
                {
                    InfoUI.text = Math.Round(2.6f + (Math.Pow(1.5, Points) / 10), 2) + " units";
                }
                break;
            case 2:
                {
                    InfoUI.text = Math.Round((11.0 + Points * 2) * 0.0147356788, 2) + " field/sec";
                }
                break;
            case 3:
                {
                    InfoUI.text = Math.Round(1.0 / (1.6f - (Math.Pow(Points, 0.9) / 5)), 2) + " bullets/sec";
                }
                break;
            case 4:
                {
                    InfoUI.text = Math.Round((7 + Points / 3.0) * 0.0913890997, 2) + " field/sec";
                }
                break;
            case 5:
                {
                    InfoUI.text = Points + ((Points == 1) ? " bottle" : " bottles");
                }
                break;
        }
    }

    public void Up()
    {
        if (Controller.Points >= Cost)
        {
            Controller.Points -= Cost;
            Points++;
            Controller.ForceUpdate();
            ForceUpdate();
        }    
    }

    public void Down()
    {
        if (Points > 1)
        {
            Controller.Points += Cost;
            Points--;
            Controller.ForceUpdate();
            ForceUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
