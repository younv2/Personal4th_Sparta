using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemSlot : MonoBehaviour
{ 
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI descTxt;
    [SerializeField] private TextMeshProUGUI quantityTxt;
    [SerializeField] private Button UseBtn;
    [SerializeField] private TextMeshProUGUI UseBtnTxt;
    private int itemId;

    public void Init(ItemData itemData)
    {
        this.itemId = itemData.Id;

        image.sprite = itemData.Sprite;
        nameTxt.text = itemData.Name;
        descTxt.text = itemData.Desc;
        quantityTxt.text = "";
        UseBtnTxt.text = "해제";
        UseBtn.onClick.RemoveAllListeners();
        UseBtn.onClick.AddListener(() => UnEquip());
    }

    public void UnEquip()
    {
        ItemData data = DataManager.Instance.GetItemData(itemId);
        if (data == null)
            return;
        GameManager.Instance.Inventory.Unequip(data.equipmentType);
    }
}
