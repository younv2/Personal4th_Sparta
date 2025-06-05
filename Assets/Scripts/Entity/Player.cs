using UnityEngine;

public class Player : EntityBase
{

    private void Awake()
    {
        stat = new PlayerStat();
        stat.Init(statDataSO.stats);
    }
}
