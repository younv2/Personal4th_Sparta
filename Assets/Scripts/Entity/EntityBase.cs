using System.Numerics;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    protected BaseAIController controller;
    protected BaseStat stat;
    protected Team team;
    public bool IsDead { get; protected set; }

    [SerializeField]protected EntityUIHealthBar hpBar;
    [SerializeField] protected StatDataSO statDataSO;
    public void Init()
    {
    }
    public bool IsEnemy(EntityBase other)
    {
        return team != other.team;
    }
    public void TakeDamage(double damage)
    {
        if(stat.TryGetStat(StatType.Hp, out var hp))
        {
            hp.SubCurrentValue(damage);
            if(hp.CurrentValue <= 0 )
            {
                Die();
            }
            hpBar?.onHealthChanged?.Invoke((float)(hp.CurrentValue/hp.FinalValue));
        }
    }
    public void Die()
    {
        IsDead = true;
        if (stat.Stats.TryGetValue(StatType.DropGold, out var dropGold))
        {
            GameManager.Instance.Inventory.AddGold((BigInteger)dropGold.FinalValue);
        }
        if (stat.Stats.TryGetValue(StatType.DropExp, out var dropExp))
        {
            PlayerStat ps = GameManager.Instance.Player.stat as PlayerStat;
            ps.AddExp((int)dropExp.FinalValue);
        }
        if (this is Monster monster)
            MonsterManager.Instance.Unregister(monster);
        Destroy(this.gameObject);

    }
}
