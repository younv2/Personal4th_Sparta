using UnityEngine;

public class Stat
{
    
    public StatType Type { get; }
    
    public float BaseValue { get; private set; }
    public float BuffValue { get; private set; }
    public float EquipmentValue { get; private set; }
    public float LevelValue {  get; private set; }

    public float FinalValue => BaseValue + EquipmentValue + BuffValue + LevelValue;

    public bool IsResource { get; private set; }
    public float CurrentValue {  get; private set; }

    public Stat()
    {
    }
    public Stat(StatType type, float baseValue = 0f, bool isResource = false)
    {
        Type = type;
        IsResource = isResource;

        BaseValue = baseValue;

        if (IsResource)
            CurrentValue = FinalValue;
    }

    public Stat(Stat stat)
    {
        Type = stat.Type;
        BaseValue = stat.BaseValue;
        BuffValue = stat.BuffValue;
        LevelValue = stat.LevelValue;
        EquipmentValue = stat.EquipmentValue;
        CurrentValue = stat.CurrentValue;
        IsResource = stat.IsResource;
    }
    #region Add
    public void AddBaseValue(float value, float min = 0f, float max = float.MaxValue)
    {
        BaseValue = Mathf.Clamp(BaseValue + value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }
    public void AddLevelValue(float value, float min = 0f, float max = float.MaxValue)
    {
        LevelValue = Mathf.Clamp(LevelValue + value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }
    public void AddEquipmentValue(float value, float min = 0f, float max = float.MaxValue)
    {
        EquipmentValue = Mathf.Clamp(EquipmentValue + value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }
    public void AddBuffValue(float value, float min = 0f, float max = float.MaxValue)
    {
        BuffValue = Mathf.Clamp(BuffValue + value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }

    public void AddCurrentValue(float value)
    {
        if (!IsResource) return;
        CurrentValue = Mathf.Clamp(CurrentValue + value, 0f, FinalValue);
    }
    #endregion

    public void SetLevelValue(float value, float min = 0f, float max = float.MaxValue)
    {
        LevelValue = Mathf.Clamp(value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }

    
    public void SetCurrentValue(float value)
    {
        if (!IsResource) return;
        CurrentValue = Mathf.Clamp(value, 0f, FinalValue);
    }

    public void SyncCurrentValue()
    {
        if (!IsResource) return;
        CurrentValue = Mathf.Clamp(CurrentValue, 0f, FinalValue);
    }

    public string GetStatName()
    {
        switch (Type)
        {
            case StatType.Attack:
                return "°ø°Ý·Â";
            case StatType.Hp:
                return "HP";
            case StatType.Mp:
                return "MP";
            default: return string.Empty;
        }
    }
}

