using UnityEngine;

public class Player : EntityBase
{
    private void Awake()
    {
        stat = new PlayerStat();
        stat.Init(statDataSO.stats);
        controller = GetComponent<PlayerController>();
        controller.Init(stat);
        team = Team.Player; 
    }
    private void Update()
    {
        Debug.Log(stat.Stats[StatType.AttackRange].FinalValue);
    }
}
