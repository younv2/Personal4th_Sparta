using System;

public class PlayerStat : BaseStat
{
    public int CurExp {  get; private set; }
    public int MaxExp { get; private set; }
    public int Level { get; private set; }

    public Action<int> onLevelChanged;
    public Action<float> onExpChanged;

    public PlayerStat() : base()
    {
        CurExp = 0;
        Level = 1;
        MaxExp = GameManager.Instance.LevelTable.GetExpPerLevel(Level);
        onExpChanged?.Invoke((float)CurExp / MaxExp);
        onLevelChanged?.Invoke(Level);
    }

    public void AddExp(int exp)
    {
        CurExp += exp;
        if (CurExp >= MaxExp)
            LevelUp();
        onExpChanged?.Invoke((float)CurExp /MaxExp);
    }
    public void LevelUp()
    {
        CurExp = 0;
        Level++;
        MaxExp = GameManager.Instance.LevelTable.GetExpPerLevel(Level);
        onLevelChanged?.Invoke(Level);
    }
    public void SetLevel(int level)
    {
        Level = level;
    }
}

