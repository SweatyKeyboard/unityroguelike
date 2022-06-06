using System.Collections;
using System.Collections.Generic;

public class Common
{
    public enum HealthType
    {
        Ketchup,
        Mustard,
        Mayo
    }

    public enum ItemType
    {
        HealthK,
        HealthMs,
        HealthMy
    }

    public struct Coords
    {
        public int x;
        public int y;

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}