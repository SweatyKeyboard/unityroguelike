using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{
    [SerializeField] string theme;
    [SerializeField] int page;
    int maxPage = 8;

    public string Theme { get { return theme; } private set { theme = value; } }
    public int Page { get { return page; } private set { page = value; } }


    void Start()
    {
        UpdatePageContent();
    }

    public void UpdatePageContent()
    {
        for (int c = 0; c < 2; c++)
        {
            if (page + c < maxPage)
            {
                string s = "Page_" + theme + "_" + GetPageItem(page+c);
                FindObjectsOfType<BookPage>()[c].SetImage(s);
            }
            else
            {
                FindObjectsOfType<BookPage>()[c].MakeTransparent();
            }
        }
    }

    string GetPageItem(int i)
    {
        if (theme == "Sauce")
            return ((Common.HealthType)(i)).ToString();
        else if (theme == "Enemies")
            return i.ToString();
        else if (theme == "Items")
            return i.ToString();
        return "";

    }

    public void NextPage()
    {
        if (page + 2 < maxPage)
        {
            page += 2;
            UpdatePageContent();
        }
    }

    public void PreviousPage()
    {
        if (page - 2 >= 0)
        {
            page -= 2;
            UpdatePageContent();
        }
    }

    public void SetTheme(string s)
    {
        theme = s;
        page = 0;

        if (theme == "Sauce")
            maxPage = 8;
        else if (theme == "Enemies")
            maxPage = 9;
        else if (theme == "Items")
            maxPage = 3;
        UpdatePageContent();
    }
}
