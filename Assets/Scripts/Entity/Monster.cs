public class Monster : EntityBase
{
    void Awake()
    {
        stat = new MonsterStat();
        stat.Init(statDataSO.stats);
        controller = GetComponent<MonsterController>();
        controller.Init(stat);
    }
}
