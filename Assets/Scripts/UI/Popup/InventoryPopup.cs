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
        {
            GameObject go = Instantiate(slotPrefab, slotParent);
            InventoryItemSlot slot = go.GetComponent<InventoryItemSlot>();
            slot.Init(DataManager.Instance.GetItemData(items[i].Id), items[i].Quantity);
            pools.Add(slot);
        }
    }

    public override void Show()
    {
        base.Show();

        for (int i = 0; i < items.Count; i++)
        {
            if (i < pools.Count)
            {
                pools[i].gameObject.SetActive(true);
                pools[i].Init(DataManager.Instance.GetItemData(items[i].Id), items[i].Quantity);
            }
            else
            {
                GameObject go = Instantiate(slotPrefab, slotParent);
                InventoryItemSlot slot = go.GetComponent<InventoryItemSlot>();
                slot.Init(DataManager.Instance.GetItemData(items[i].Id), items[i].Quantity);
                pools.Add(slot);
            }
        }

        // 남은 슬롯 비활성화
        for (int i = items.Count; i < pools.Count; i++)
        {
            pools[i].gameObject.SetActive(false);
        }
    }
}