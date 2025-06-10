using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI descTxt;
    [SerializeField] private TextMeshProUGUI priceTxt;
    private int itemId;
    private int price;
    private void OnEnable()
    {
        LackMoney(GameManager.Instance.Inventory.Gold);
        GameManager.Instance.Inventory.onGoldChanged += LackMoney;
    }
    private void OnDisable()
    {
        if(GameManager.Instance != null)
        GameManager.Instance.Inventory.onGoldChanged -= LackMoney;
    }
    public void Init(ItemData itemData,int price)
    {
        this.itemId = itemData.Id;
        this.price = price;

        image.sprite = itemData.Sprite;
        nameTxt.text = itemData.Name;
        descTxt.text = itemData.Desc;
        priceTxt.text = $"{price} G";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.Instance.Inventory.SpendGold(price))
        {
            GameManager.Instance.Inventory.AddItem(itemId);
        }
    }
    public void LackMoney(BigInteger value)
    {
        if(price > value)
        {
            priceTxt.color = Color.red;
        }
        else
        {
            priceTxt.color = Color.black;
        }
    }
}
