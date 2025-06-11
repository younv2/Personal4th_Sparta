using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private StageDataSO stageDataSO;
    
    public List<RoomArea> allRooms = new();
    private int currentStage = 1;

    public int CurrentStage { get { return currentStage; } }
    public Action<int> onStageChanged;

    public void StartStage(int level)
    {
        currentStage = level;
        GameManager.Instance.Player.transform.position = Vector3.up;
        GameManager.Instance.Player.GetComponent<NavMeshAgent>().enabled = true;
        GameManager.Instance.Player.GetComponent<FSMController>().enabled = true;
        StageInfo stageInfo = stageDataSO.Stages.FirstOrDefault(x=>x.stageKey == level);
        if (stageInfo == null)
        {
            return;            
        }
        for (int i = 0; i < stageInfo.monsters.Length; i++)
        {
            for(int j = 0; j< stageInfo.monsters[i].spawnCount; j++)
            {
                MonsterFactory.Instance.Create(stageInfo.monsters[i].monsterType, NavMeshUtil.GetRandomPointOnNavMesh(x => x == RoomType.Normal, allRooms), Quaternion.identity);
            }
        }
        if (stageInfo.hasBoss)
            StartCoroutine(SpawnBossMonster(stageInfo.bossType));
        onStageChanged?.Invoke(level);
    }
    IEnumerator SpawnBossMonster(MonsterType monsterType)
    {
        yield return new WaitUntil(() => MonsterManager.Instance.GetMonsterCount() == 0);

        MonsterFactory.Instance.Create(monsterType, NavMeshUtil.GetRandomPointOnNavMesh(x => x == RoomType.Boss,allRooms), Quaternion.identity);

        yield return new WaitUntil(() => MonsterManager.Instance.GetMonsterCount() == 0);
        StartStage(currentStage + 1);
        SaveManager.Instance.Save();
    }
}

