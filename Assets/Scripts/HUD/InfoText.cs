using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{

    struct Message
    {
        public string text;
        public float length;
        public Color color;

        public Message(string s, float l)
        {
            text = s;
            length = l;
            color = new Color(1,1,1,1);
        }

        public Message(string s, float l, Color c)
        {
            text = s;
            length = l;
            color = c;
        }
    }

    Queue<Message> queue = new Queue<Message>();
    bool showingNow = false;

    public void TryShow()
    {
        if (!showingNow)
        {
            showingNow = true;
            StartCoroutine(Timer());
        }
    }

    public void ShowText(string text, float time)
    {
        Message m = new Message(text, time);
        queue.Enqueue(m);
        TryShow();
    }

    public void ShowText(string text, float time, Color col)
    {
        queue.Enqueue(new Message(text, time, col));
        TryShow();
    }

    public void ShowText(string text)
    {
        if (!showingNow)
        {
            GetComponent<Text>().text = text;
            GetComponent<Text>().color = Color.white;
        }
    }

    public void HideText()
    {
        if (!showingNow)
        {
            GetComponent<Text>().color = new Color(1, 1, 1, 0);
            GetComponent<Text>().text = "";
        }
    }

    IEnumerator Timer()
    {
        while (queue.Count > 0)
        {
            Message m = queue.Dequeue();            
            GetComponent<Text>().text = m.text;
            GetComponent<Text>().color = m.color;
            Color cc = GetComponent<Text>().color;            

            yield return new WaitForSeconds(m.length);            
        }       
        showingNow = false;
        HideText();
        yield break;
    }

}
