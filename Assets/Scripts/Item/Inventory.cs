using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Inventory
{
    private BigInteger gold;
    private List<Item> items = new();
    private Dictionary<EquipmentType, Item> equipment = new();

    public Action onInventoryChanged;
    public Action<BigInteger> onGoldChanged;

    public List<Item> Items {  get { return items; } }
    public BigInteger Gold { get { return gold; } }
    public Dictionary<EquipmentType, Item> Equipment { get {  return equipment; } }

    public Item GetItem(int itemId)
    {
        return items.Find(x => x.Id == itemId);
    }
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
    public void AddItem(int itemId)
    {
        ItemData item = DataManager.Instance.GetItemData(itemId);
        foreach (var data in items)
        {
            if (data.Id == item.Id && item.isStackable && data.Quantity < item.maxStackCount)
            {
                data.AddStack();
                Debug.Log("아이템 갯수 추가");
                return;
            }
        }
        items.Add(new Item(itemId));
        Debug.Log("아이템 추가");
    }
    public void UseItem(int itemId)
    {
        Item item = items.Find(x => x.Id == itemId);
        item.Use();

        item.Remove();

        if (item.Quantity <= 0)
        {
            items.Remove(item);
        }
        onInventoryChanged?.Invoke();
    }
    public bool Equip(int itemId)
    {
        var data = DataManager.Instance.GetItemData(itemId);
        if (data.type != ItemType.Equipment)
            return false;

        EquipmentType type = data.equipmentType;

        // 기존 아이템이 있는 경우 먼저 해제
        if (equipment.TryGetValue(type, out var current) && current != null)
        {
            AddItem(current.Id);
            var unequippedData = DataManager.Instance.GetItemData(current.Id);
            foreach (var stat in unequippedData.stats)
            {
                GameManager.Instance.Player.Stat.Stats[stat.type].SubEquipmentValue(stat.baseValue);
            }
        }
        Item item = GetItem(itemId);
        if (item == null) return false;
        equipment[type] = item;
        foreach (var stat in data.stats)
        {
            GameManager.Instance.Player.Stat.Stats[stat.type].AddEquipmentValue(stat.baseValue);
        }
        // 인벤토리에서 장착한 아이템 제거
        items.Remove(item);

        onInventoryChanged?.Invoke();
        return true;
    }

    public bool Unequip(EquipmentType type)
    {
        if (!equipment.TryGetValue(type, out var item) || item == null)
            return false;

        AddItem(item.Id); // 장비를 인벤토리로
        equipment[type] = null;
        var unequippedData = DataManager.Instance.GetItemData(item.Id);
        foreach (var stat in unequippedData.stats)
        {
            GameManager.Instance.Player.Stat.Stats[stat.type].SubEquipmentValue(stat.baseValue);
        }
        onInventoryChanged?.Invoke();
        return true;
    }
}
