using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StatData", menuName = "Scriptable Objects/StatData")]
public class StatDataSO : ScriptableObject
{
    public List<StatData> stats;
}

[System.Serializable]
public class StatData
{
    public StatType type;
    public float baseValue;
    public bool isResource;
}