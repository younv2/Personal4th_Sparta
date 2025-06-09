using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private StageDataSO stageDataSO;
    [SerializeField] private GameObject playerPrefab;
    public List<RoomArea> allRooms = new();
    private int currentStage = 1;
    public void StartStage(int level)
    {
        currentStage = level;
        if (GameManager.Instance.Player == null)
        {
            GameObject go = Instantiate(playerPrefab, Vector3.up, Quaternion.identity);
            GameManager.Instance.Player = go.GetComponent<Player>();
        }
        else
            GameManager.Instance.Player.transform.position = Vector3.up;
        StageInfo stageInfo = stageDataSO.Stages.FirstOrDefault(x=>x.stageKey == level);
        if (stageInfo == null)
        {
            return;            
        }
        for (int i = 0; i < stageInfo.monsters.Length; i++)
        {
            for(int j = 0; j< stageInfo.monsters[i].spawnCount; j++)
            {
                MonsterFactory.Instance.Create(stageInfo.monsters[0].monsterType, NavMeshUtil.GetRandomPointOnNavMesh(x => x == RoomType.Normal, allRooms), Quaternion.identity);
            }
        }
        if (stageInfo.hasBoss)
            StartCoroutine(SpawnBossMonster(stageInfo.bossType));
        UIManager.Instance.HUD.onStageChanged?.Invoke(level);
    }
    IEnumerator SpawnBossMonster(MonsterType monsterType)
    {
        yield return new WaitUntil(() => MonsterManager.Instance.GetMonsterCount() == 0);

        MonsterFactory.Instance.Create(monsterType, NavMeshUtil.GetRandomPointOnNavMesh(x => x == RoomType.Boss,allRooms), Quaternion.identity);

        yield return new WaitUntil(() => MonsterManager.Instance.GetMonsterCount() == 0);

        StartStage(currentStage + 1);
    }
}

