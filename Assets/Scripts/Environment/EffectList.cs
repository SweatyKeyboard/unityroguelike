using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class EffectList
{
    List<Common.Effects> effect;
    List<float> duration;

    public EffectList()
    {
        effect = new List<Common.Effects>();
        duration = new List<float>();
    }

    public void Add(Common.Effects e, float d)
    {
        effect.Add(e);
        duration.Add(d);
    }

    public void Remove(Common.Effects e)
    {
        int index = effect.IndexOf(e);

        effect.RemoveAt(index);
        duration.RemoveAt(index);
    }

    public bool Contiains(Common.Effects e)
    {
        if (effect.Contains(e))
            return true;
        return false;
    }

    public Common.Effects this[int i]
    {
        get { return effect[i]; }
    }
}
