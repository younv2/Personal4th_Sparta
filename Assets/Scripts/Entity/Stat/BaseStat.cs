using System.Collections.Generic;
using Unity.VisualScripting;

public class BaseStat
{
    public string Name { get; private set; }
    public Dictionary<StatType, Stat> Stats { get; private set; } = new();

    public BaseStat()
    {
        Name = "";
        Stats.Add(StatType.Attack, new Stat(StatType.Attack, 5));
        Stats.Add(StatType.Hp, new Stat(StatType.Hp, 50,true));
        Stats.Add(StatType.MoveSpeed, new Stat(StatType.MoveSpeed, 5));
    }
    public Stat GetStat(StatType type)
    {
        return Stats.TryGetValue(type, out var stat) ? stat : null;
    }
    public bool TryGetStat(StatType type, out Stat stat)
    {
        return Stats.TryGetValue(type, out stat);
    }
    public void Init(List<StatData> datas)
    {
        Stats.Clear();

        foreach (StatData data in datas)
        {
            Stats[data.type] = new Stat(data.type, data.baseValue, data.isResource);
        }
    }
}

