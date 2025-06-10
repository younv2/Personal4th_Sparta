using System.Collections;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    [SerializeField] private ItemDataTable itemDataTable;

    public ItemData GetItemData(int id)
    {
        return itemDataTable.itemDatas.Find(x=>x.Id == id);
    }
}
