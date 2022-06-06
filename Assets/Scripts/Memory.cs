using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Memory
{
    public int itemId;
    public Vector3 position;
    public Quaternion rotation;

    public Memory(int i, Vector3 p, Quaternion q)
    {
        itemId = i;
        position = p;
        rotation = q;
    }
}
