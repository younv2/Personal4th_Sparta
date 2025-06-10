using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public int Id;
    public string Name;
    public string Desc;
    public Sprite Sprite;
    public ItemType type;
    public bool isStackable;
    public int maxStackCount;
    public bool isImmediately;
    public float time;
    public List<StatData> stats;
}
public enum ItemType
{
    Consume, Equipment
} 