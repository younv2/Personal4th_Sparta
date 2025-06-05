using UnityEngine;
using System.Linq;
public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private StageDataSO stageDataSO;
    public void StartStage(int level)
    {
        StageInfo stageInfo = stageDataSO.Stages.FirstOrDefault(x=>x.stageKey == level);

        MonsterFactory.Instance.Create(stageInfo.waves[0].monsters[0].monsterType,Vector3.zero,Quaternion.identity);
    }
}

