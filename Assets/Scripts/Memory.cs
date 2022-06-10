using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MemoryItem
{
    public int itemId;
    public Vector3 position;
    public Quaternion rotation;

    public MemoryItem(int i, Vector3 p, Quaternion q)
    {
        itemId = i;
        position = p;
        rotation = q;
    }
}

public class MemoryInteractive
{
    public Common.InteractiveType type;
    public bool boolean;
    public int integer;
    public string str;

    public MemoryInteractive(Common.InteractiveType t, bool b)
    {
        type = t;
        boolean = b;
    }
    public MemoryInteractive(Common.InteractiveType t, int i)
    {
        type = t;
        integer = i;
    }

    public MemoryInteractive(Common.InteractiveType t, string s)
    {
        type = t;
        str = s;
    }

    public MemoryInteractive(Common.InteractiveType t, int i, string s)
    {
        type = t;
        str = s;
        integer = i;
    }
}
