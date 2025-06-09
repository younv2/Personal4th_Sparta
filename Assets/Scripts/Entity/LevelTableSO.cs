using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/LevelTable")]
public class LevelTableSO : ScriptableObject
{
    public List<LevelData> levelDatas;

    public int GetExpPerLevel(int level)
    {
        return levelDatas.Find(x => x.level == level).requireExp;
    }
}
[System.Serializable]
public class LevelData
{
    public int level;
    public int requireExp;
}
