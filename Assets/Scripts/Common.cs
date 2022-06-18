using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common
{
    public enum HealthType
    {
        Void = -1,
        Ketchup,
        Mayo,
        Cheese,
        Soy,        
        Bbq,
        Garlic,
        Mustard,
        Chili
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
        Burger,
        HealthSoy,
        HealthBbq,
        HealthGar,
        HealthChl,
        HealthChs
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

    public enum Effects
    {
        Poison,
        Slowdown,
        ShotSpeedUp,
        SpeedsUp
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