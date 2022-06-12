using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class Matrix
{
    public static int[,] DistanceMatrix(int[,] matrix, List<Common.Coords> from)
    {
        int[,] answer = new int[matrix.GetLength(0), matrix.GetLength(1)];
        for (int x = 0; x < matrix.GetLength(0); x++)
            for (int y = 0; y < matrix.GetLength(1); y++)
                if (matrix[x, y] != 0)
                    answer[x, y] = -1;
                else
                    answer[x, y] = -2;
        foreach (Common.Coords coord in from)
            answer[coord.x, coord.y] = 0;

        for (int d = 0; Count(answer, -1) > 0; d++)
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    if (answer[x, y] == d)
                    {
                        for (int xx = -1; xx <= 1; xx++)
                            for (int yy = -1; yy <= 1; yy++)
                            {
                                if (Math.Abs(xx) != Math.Abs(yy))
                                {
                                    try
                                    {
                                        if (answer[x + xx, y + yy] == -1)
                                            answer[x + xx, y + yy] = d + 1;
                                    }
                                    catch { continue; }
                                }
                            }
                    }
                }
        }

        return answer;
    }

    public static int Max(int[,] matrix)
    {
        int max = int.MinValue;
        for (int x = 0; x < matrix.GetLength(0); x++)
            for (int y = 0; y < matrix.GetLength(1); y++)
                if (matrix[x, y] > max)
                    max = matrix[x, y];
        return max;
    }


    public static int Count(int[,] matrix, int i)
    {
        int counter = 0;
        for (int x = 0; x < matrix.GetLength(0); x++)
            for (int y = 0; y < matrix.GetLength(1); y++)
                if (matrix[x, y] == i)
                    counter++;
        return counter;
    }

    public static List<Common.Coords> PositionsOf(int[,] matrix, int i)
    {
        List<Common.Coords> positions = new List<Common.Coords>();
        for (int x = 0; x < matrix.GetLength(0); x++)
            for (int y = 0; y < matrix.GetLength(1); y++)
                if (matrix[x, y] == i)
                    positions.Add(new Common.Coords(x, y));

        return positions;
    }

    public static Common.Coords RandomFarthest(int[,] matrix, List<Common.Coords> from)
    {
        int[,] distances = DistanceMatrix(matrix, from);
        int max = Max(distances);
        List<Common.Coords> list = PositionsOf(distances, max);

        int rand = UnityEngine.Random.Range(0, list.Count);

        return new Common.Coords(list[rand].x, list[rand].y);
    }

    public static Common.Coords RandomFarthestExcept(int[,] matrix, List<Common.Coords> from, List<Common.Coords> except)
    {
        Common.Coords res;
        int counter = 0;
        do
        {
            int[,] distances = DistanceMatrix(matrix, from);
            int max = Max(distances);
            if (counter > 3)
                max--;
            List<Common.Coords> list = PositionsOf(distances, max);

            int rand = UnityEngine.Random.Range(0, list.Count);
            res = new Common.Coords(list[rand].x, list[rand].y);
            counter++;
        } while (except.Contains(res));

        return res;
    }

    public static string Print(int[,] matrix)
    {
        string s = "";
        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            s += "\n";
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                s += matrix[x, y] + " ";
            }
        }
        return s;
    }
}

