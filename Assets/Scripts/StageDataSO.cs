using UnityEngine;

[System.Serializable]
public class StageInfo
{
    public int stageKey;
    public WaveData[] waves;

    public StageInfo(int stageKey, WaveData[] waves)
    {
        this.stageKey = stageKey;
        this.waves = waves;
    }
}

[System.Serializable]
public class WaveData
{
    public MonsterSpawnData[] monsters;
    public bool hasBoss;
    public MonsterType bossType;

    public WaveData(MonsterSpawnData[] monsters, bool hasBoss, MonsterType bossType)
    {
        this.monsters = monsters;
        this.hasBoss = hasBoss;
        this.bossType = bossType;
    }
}

[System.Serializable]
public class MonsterSpawnData
{
    public MonsterType monsterType;
    public int spawnCount;

    public MonsterSpawnData(MonsterType monsterType, int spawnCount)
    {
        this.monsterType = monsterType;
        this.spawnCount = spawnCount;
    }
}
[CreateAssetMenu(menuName = "Scriptable Objects/Stage Data")]
public class StageDataSO : ScriptableObject
{
    public StageInfo[] Stages;

}
