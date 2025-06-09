public class PlayerStat : BaseStat
{
    public int CurExp {  get; private set; }
    public int MaxExp { get; private set; }
    public int Level { get; private set; }
    public PlayerStat() : base()
    {
        CurExp = 0;
        Level = 1;
        MaxExp = GameManager.Instance.LevelTable.GetExpPerLevel(Level);
        UIManager.Instance.HUD.onExpChanged?.Invoke((float)CurExp / MaxExp);
        UIManager.Instance.HUD.onLevelChanged?.Invoke(Level);
    }

    public void AddExp(int exp)
    {
        CurExp += exp;
        if (CurExp >= MaxExp)
            LevelUp();
        UIManager.Instance.HUD.onExpChanged?.Invoke((float)CurExp /MaxExp);
    }
    public void LevelUp()
    {
        CurExp = 0;
        Level++;
        MaxExp = GameManager.Instance.LevelTable.GetExpPerLevel(Level);
        UIManager.Instance.HUD.onLevelChanged?.Invoke(Level);
    }
}

