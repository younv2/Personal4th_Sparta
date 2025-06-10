using System.Collections.Generic;
using UnityEngine;

public class InventoryPopup : BasePopup
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;

    private List<Item> items;
    private List<InventoryItemSlot> pools = new();

    public override void Initialize()
    {
        base.Initialize();
        items = GameManager.Instance.Inventory.Items;

        for (int i = 0; i < items.Count; i++)
            AddSlotToPool();
    }

    public override void Show()
    {
        base.Show();

        items = GameManager.Instance.Inventory.Items;

        // 1) 풀 확장
        while (pools.Count < items.Count)
            AddSlotToPool();

        // 2) 슬롯 갱신
        for (int i = 0; i < pools.Count; i++)
        {
            if (i < items.Count)
            {
                var data = DataManager.Instance.GetItemData(items[i].Id);
                pools[i].Init(data, items[i].Quantity);
                pools[i].gameObject.SetActive(true);
            }
            else
            {
                pools[i].gameObject.SetActive(false);
            }
        }
    }

    private void AddSlotToPool()
    {
        var go = Object.Instantiate(slotPrefab, slotParent);
        var slot = go.GetComponent<InventoryItemSlot>();
        pools.Add(slot);
    }
}