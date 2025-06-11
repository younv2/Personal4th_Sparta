using UnityEngine;

public class ShopPopup : BasePopup
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private ShopData shopData;

    public override void Initialize()
    {
        base.Initialize();

        for(int i = 0;i<shopData.itemDatas.Count;i++)
        {

            GameObject go = Instantiate(slotPrefab, slotParent);
            go.GetComponent<ShopItemSlot>().Init(shopData.itemDatas[i].itemId, shopData.itemDatas[i].price);
        }
    }
    public override void Show()
    {
        base.Show();
        SoundManager.Instance.PlaySound(SoundType.SFX, "Shoot", false);
    }
}