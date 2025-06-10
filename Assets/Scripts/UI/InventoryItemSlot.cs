using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{ 
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI descTxt;
    [SerializeField] private TextMeshProUGUI quantityTxt;
    [SerializeField] private Button UseBtn;
    [SerializeField] private TextMeshProUGUI UseBtnTxt;
    private int itemId;

    public void Init(ItemData itemData,int quantity)
    {
        this.itemId = itemData.Id;

        image.sprite = itemData.Sprite;
        nameTxt.text = itemData.Name;
        descTxt.text = itemData.Desc;
        quantityTxt.text = itemData.isStackable ? $"x {quantity}" : "";
        UseBtnTxt.text = itemData.type == ItemType.Equipment ? "장착" : "사용";
        UseBtn.onClick.RemoveAllListeners();
        UseBtn.onClick.AddListener(() => Use());
    }

    public void Use()
    {
        ItemData data = DataManager.Instance.GetItemData(itemId);
        if (data == null)
            return;
        if(data.type== ItemType.Equipment)
        {
            GameManager.Instance.Inventory.Equip(itemId);
        }
        else
        {
            GameManager.Instance.Inventory.UseItem(itemId);

        }
    }
}
