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
            color = Color.white;
        }

        public Message(string s, float l, Color c)
        {
            text = s;
            length = l;
            color = c;
        }
    }

    Queue<Message> queue = new Queue<Message>();
    bool showingNow;
    float endTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!showingNow)
        {
            if (queue.Count > 0)
            {
                showingNow = true;
                Message m = queue.Dequeue();
                endTime = Time.time + m.length;

                GetComponent<Text>().text = m.text;
                GetComponent<Text>().color = m.color;
            }
        }
        else
        if (Time.time > endTime)
        {
            HideText();
        }
    }

    public void ShowText(string text, float time)
    {
        queue.Enqueue(new Message(text, time));
    }

    public void ShowText(string text, float time, Color col)
    {
        queue.Enqueue(new Message(text, time, col));
    }

    public void ShowText(string text)
    {
        if (queue.Count == 0)
        {
            GetComponent<Text>().text = text;
            GetComponent<Text>().color = Color.white;
        }
    }

    public void HideText()
    {
        GetComponent<Text>().color = new Color(1, 1, 1, 0);
        showingNow = false;
    }

}
