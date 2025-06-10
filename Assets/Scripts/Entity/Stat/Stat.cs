using System;
using UnityEngine;

public class Stat
{
    public StatType Type { get; }
    
    public double BaseValue { get; private set; }
    public double BuffValue { get; private set; }
    public double EquipmentValue { get; private set; }
    public double LevelValue {  get; private set; }

    public double FinalValue => BaseValue + EquipmentValue + BuffValue + LevelValue;

    public bool IsResource { get; private set; }
    public double CurrentValue {  get; private set; }

    public Stat()
    {
    }
    public Stat(StatType type, double baseValue = 0f, bool isResource = false)
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
    public void AddBaseValue(double value, double min = 0f, double max = double.MaxValue)
    {
        BaseValue = Math.Clamp(BaseValue + value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }
    public void AddLevelValue(double value, double min = 0f, double max = double.MaxValue)
    {
        LevelValue = Math.Clamp(LevelValue + value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }
    public void AddEquipmentValue(double value, double min = 0f, double max = double.MaxValue)
    {
        EquipmentValue = Math.Clamp(EquipmentValue + value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }
    public void AddBuffValue(double value, double min = 0f, double max = double.MaxValue)
    {
        BuffValue = Math.Clamp(BuffValue + value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }

    public void AddCurrentValue(double value)
    {
        if (!IsResource) return;
        CurrentValue = Math.Clamp(CurrentValue + value, 0f, FinalValue);
    }
    #endregion
    #region Sub
    public void SubBaseValue(double value, double min = 0f, double max = double.MaxValue)
    {
        BaseValue = Math.Clamp(BaseValue - value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }
    public void SubLevelValue(double value, double min = 0f, double max = double.MaxValue)
    {
        LevelValue = Math.Clamp(LevelValue - value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }
    public void SubEquipmentValue(double value, double min = 0f, double max = double.MaxValue)
    {
        EquipmentValue = Math.Clamp(EquipmentValue - value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }
    public void SubBuffValue(double value, double min = 0f, double max = double.MaxValue)
    {
        BuffValue = Math.Clamp(BuffValue - value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }

    public void SubCurrentValue(double value)
    {
        if (!IsResource) return;
        CurrentValue = Math.Clamp(CurrentValue - value, 0f, FinalValue);
    }
    #endregion

    public void SetLevelValue(double value, double min = 0f, double max = double.MaxValue)
    {
        LevelValue = Math.Clamp(value, min, max);
        if (IsResource)
            SyncCurrentValue();
    }

    
    public void SetCurrentValue(double value)
    {
        if (!IsResource) return;
        CurrentValue = Math.Clamp(value, 0f, FinalValue);
    }

    public void SyncCurrentValue()
    {
        if (!IsResource) return;
        CurrentValue = Math.Clamp(CurrentValue, 0f, FinalValue);
    }

    public string GetStatName()
    {
        switch (Type)
        {
            case StatType.Attack:
                return "공격력";
            case StatType.Hp:
                return "HP";
            case StatType.Mp:
                return "MP";
            default: return string.Empty;
        }
    }
}

