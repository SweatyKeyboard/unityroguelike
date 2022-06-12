using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpDown : MonoBehaviour
{

    public Sprite Icon;
    public string Name;
    public string Info;
    public int Points;
    public PointsController Controller;
    public int Cost;

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
        InfoUI.text = Info;
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
