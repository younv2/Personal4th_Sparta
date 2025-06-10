using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{ 
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI descTxt;
    [SerializeField] private TextMeshProUGUI quantityTxt;
    private int itemId;

    public void Init(ItemData itemData,int quantity)
    {
        this.itemId = itemData.Id;

        image.sprite = itemData.Sprite;
        nameTxt.text = itemData.Name;
        descTxt.text = itemData.Desc;
        quantityTxt.text = $"x {quantity}";
    }
}
