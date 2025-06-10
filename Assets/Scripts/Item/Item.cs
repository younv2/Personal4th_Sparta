using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private int id;
    private int quantity;

    public int Id {  get { return id; } }
    public int Quantity { get { return quantity; } }
    public Item(int id)
    {
        this.id = id;
        this.quantity = 1;
    }
    public void AddStack()
    {
        quantity++;
    }
    public void Use()
    {
        ItemData item = DataManager.Instance.GetItemData(id);
        if (item == null)
            return;

        if(item.type == ItemType.Consume)
        {
            if(item.isImmediately)
            {
                foreach(var data in item.stats)
                {
                    if(data.isResource)
                        GameManager.Instance.Player.Stat.Stats[data.type].AddCurrentValue(data.baseValue);
                    else
                        GameManager.Instance.Player.Stat.Stats[data.type].AddBaseValue(data.baseValue);
                }
            }
            else
            {
                GameManager.Instance.StartBuffCoroutine(BuffCoroutine(item.stats,item.time));
            }
        }
        if(item.type == ItemType.Equipment)
        {

        }
    }
    public void Remove()
    {
        if (quantity >= 1)
            quantity--;
    }

    IEnumerator BuffCoroutine(List<StatData> data, float time)
    {
        foreach(var stat in data)
        {
            GameManager.Instance.Player.Stat.Stats[stat.type].AddBuffValue(stat.baseValue);
        }
        yield return new WaitForSecondsRealtime(time);
        foreach (var stat in data)
        {
            GameManager.Instance.Player.Stat.Stats[stat.type].SubBuffValue(stat.baseValue);
        }
    }
}
