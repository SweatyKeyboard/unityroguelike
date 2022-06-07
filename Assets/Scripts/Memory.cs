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
    public bool condition;

    public MemoryInteractive(Common.InteractiveType t, bool b)
    {
        type = t;
        condition = b;
    }
}
