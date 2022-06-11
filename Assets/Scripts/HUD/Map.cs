using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Map
{
    public int[,] Prototypes;
    public bool[,] Visited;
    public bool[,] Known;

    public Common.Coords bossCoords;
    public Common.Coords shopCoords;
    public Map(int[,] protoypes)
    {
        int size = protoypes.GetLength(0);

        Prototypes = protoypes;
        Visited = new bool[size, size];
        Known = new bool [size, size];

        Visited[size / 2, size / 2] = true;
        Known[size / 2, size / 2] = true;
        for (int xx = -1; xx <= 1; xx++)
            for (int yy = -1; yy <= 1; yy++)
            {
                if (Math.Abs(xx) != Math.Abs(yy))
                {
                    try
                    {
                        if (Prototypes[size/2 + xx, size/2 + yy] != 0)
                            Known[size/2 + xx, size/2 + yy] = true;
                    }
                    catch { continue; }
                }
            }
    }

    public Common.Coords Find(int i )
    {
        return Matrix.PositionsOf(Prototypes, i)[0];
    }

    public void SetBoss(Common.Coords coords)
    {
        bossCoords = coords;
    }

    public void SetShop(Common.Coords coords)
    {
        shopCoords = coords;
    }
}
