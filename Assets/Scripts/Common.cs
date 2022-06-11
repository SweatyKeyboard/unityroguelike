using System.Collections;
using System.Collections.Generic;

public class Common
{
    public enum HealthType
    {
        Void = -1,
        Ketchup,
        Mayo,
        Mustard,
    }

    public enum ItemType
    {
        HealthK,
        HealthMs,
        HealthMy,
        Salt,
        Pepper,
        Cucumber,
        Tomato,
        Cheese,
        Salad,
        Bacon,
        Burger
    }

    public enum InteractiveType
    {
        Stove,
        Cutlery,
        ShopItem
    }

    public enum RoamingType
    {
        FollowPlayer,
        Random
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

        public static bool operator ==(Coords c1, Coords c2)
        {
            if (c1.x == c2.x && c1.y == c2.y)
                return true;
            else
                return false;
        }

        public static bool operator !=(Coords c1, Coords c2)
        {
            if (c1.x == c2.x && c1.y == c2.y)
                return false;
            else
                return true;
        }
    }
}