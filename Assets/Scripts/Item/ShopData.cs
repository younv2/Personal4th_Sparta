using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Scriptable Objects/ShopData")]
public class ShopData : ScriptableObject
{
    public List<PriceData> itemDatas;
}
[System.Serializable]
public struct PriceData
{
    public ItemData itemId;
    public int price;
}
