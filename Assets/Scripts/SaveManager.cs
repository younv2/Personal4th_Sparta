using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    private readonly string path = SystemPath.GetPath("save.json");
    private SaveData data = new();

    protected override void Awake()
    {
        base.Awake();
    }
    public void Save()
    {
        PlayerStat stat = GameManager.Instance.Player.Stat as PlayerStat;
        data.level = stat.Level;
        data.stage = StageManager.Instance.CurrentStage;
        data.gold = GameManager.Instance.Inventory.Gold.ToString();

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
    public SaveData Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.Log("저장 데이터가 존재하지 않음");
            return new SaveData();
        }
    }
}
[System.Serializable]
public class SaveData
{
    public int level;
    public int stage;
    public string gold;

    public SaveData() 
    {
        level = 1;
        stage = 1;
        gold = "0";
    }

}

