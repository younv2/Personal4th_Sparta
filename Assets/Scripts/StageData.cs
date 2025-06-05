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

public static class StageData
{
    public static readonly StageInfo[] Stages = new StageInfo[]
    {
        new StageInfo(0, new WaveData[]
        {
            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData(MonsterType.RedCapsule,1),
            }
            ,false,MonsterType.None),

            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData(MonsterType.RedCapsule, 3),
            }
            ,false,MonsterType.None),

            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData(MonsterType.RedCapsule,2),
                new MonsterSpawnData(MonsterType.RedCapsule,2),
                new MonsterSpawnData(MonsterType.RedCapsule,2),
            }
            ,true,MonsterType.RedSquare),
        }
        ),

        new StageInfo(1, new WaveData[]
        {
            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData(MonsterType.RedCapsule,5),
            }
            ,false,MonsterType.None),

            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData(MonsterType.RedCapsule, 3),
            }
            ,false,MonsterType.None),

            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData(MonsterType.RedCapsule,3),
                new MonsterSpawnData(MonsterType.GreenCapsule,3),
            }
            ,true,MonsterType.RedSquare),
        }
        ),
    };

}
