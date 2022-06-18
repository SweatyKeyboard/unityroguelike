using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class GlobalEffects
{
    public static void AddEffectForAllEnemies(Common.Effects effetct, float duration)
    {
        foreach (Enemy g in GameObject.FindObjectsOfType<Enemy>())
        {
            g.AddEffect(effetct, duration);
        }
    }
}
