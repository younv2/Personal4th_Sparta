using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EquipmentPopup : BasePopup
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private TextMeshProUGUI attackPowerTxt;
    [SerializeField] private TextMeshProUGUI attackRangeTxt;
    [SerializeField] private TextMeshProUGUI attackSpeedTxt;

    private List<EquipmentItemSlot> pools = new();
    Dictionary<EquipmentType, Item>.ValueCollection items;
    public override void Initialize()
    {
        base.Initialize();
        items = GameManager.Instance.Inventory.Equipment.Values;
        
        for (int i = 0; i < items.Count; i++)
            AddSlotToPool();
        GameManager.Instance.Inventory.onInventoryChanged += SyncEquipment;
    }
    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        GameManager.Instance.Inventory.onInventoryChanged -= SyncEquipment;
    }
    public override void Show()
    {
        base.Show();
        SoundManager.Instance.PlaySound(SoundType.SFX, "Shoot", false);
        items = GameManager.Instance.Inventory.Equipment.Values;

        while (pools.Count < items.Count)
            AddSlotToPool();
        SyncStat();
        SyncEquipment();
    }
    public void SyncEquipment()
    {
        List<Item> itemList = items.ToList();
        for (int i = 0; i < pools.Count; i++)
        {
            if (i < items.Count && itemList[i] != null)
            {
                var data = DataManager.Instance.GetItemData(itemList[i].Id);
                pools[i].Init(data);
                pools[i].gameObject.SetActive(true);
            }
            else
            {
                pools[i].gameObject.SetActive(false);
            }
        }
    }
    public void SyncStat()
    {
        attackPowerTxt.text = "공격력 " + GameManager.Instance.Player.Stat.Stats[StatType.Attack].FinalValue.ToString();
        attackRangeTxt.text = "공격 범위 " + GameManager.Instance.Player.Stat.Stats[StatType.AttackRange].FinalValue.ToString();
        attackSpeedTxt.text = "공격 속도 " + GameManager.Instance.Player.Stat.Stats[StatType.AttackDelay].FinalValue.ToString();
    }
    private void AddSlotToPool()
    {
        var go = Object.Instantiate(slotPrefab, slotParent);
        var slot = go.GetComponent<EquipmentItemSlot>();
        pools.Add(slot);
    }
}