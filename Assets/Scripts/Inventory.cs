using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Inventory
{
    private BigInteger gold;
    private List<Item> items = new();
    public BigInteger Gold { get { return gold; } }

    public Action<BigInteger> onGoldChanged;

    public void AddGold(BigInteger gold)
    {
        this.gold += gold;
        onGoldChanged?.Invoke(this.gold);
    }
    public bool SpendGold(BigInteger gold)
    {
        if(this.gold < gold)
        {
            return false;
        }
        this.gold -= gold;
        onGoldChanged?.Invoke(this.gold);
        return true;
    }

    public void AddItem(ItemData item)
    {
        foreach(var data in items)
        {
            if(data.Id == item.Id && item.isStackable && data.Stack < item.maxStackCount)
            {
                data.AddStack();
                return;
            }
        }
        items.Add(new Item(item.Id));
    }
    public void AddItem(int itemId)
    {
        ItemData item = DataManager.Instance.GetItemData(itemId);
        foreach (var data in items)
        {
            if (data.Id == item.Id && item.isStackable && data.Stack < item.maxStackCount)
            {
                data.AddStack();
                Debug.Log("아이템 갯수 추가");
                return;
            }
        }
        items.Add(new Item(itemId));
        Debug.Log("아이템 추가");
    }
}
