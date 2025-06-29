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
        GameManager.Instance.Inventory.onInventoryChanged += SyncInventory;
    }
    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        GameManager.Instance.Inventory.onInventoryChanged -= SyncInventory;
    }
    public override void Show()
    {
        base.Show();
        SoundManager.Instance.PlaySound(SoundType.SFX, "Shoot", false);
        items = GameManager.Instance.Inventory.Items;

        while (pools.Count < items.Count)
            AddSlotToPool();

        SyncInventory();
    }
    public void SyncInventory()
    {
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