using UnityEngine;

public class EntityBase : MonoBehaviour
{
    protected BaseAIController controller;
    protected BaseStat stat;
    protected Team team;
    public bool IsDead { get; protected set; }

    [SerializeField] protected StatDataSO statDataSO;
    public void Init()
    {
    }
    public bool IsEnemy(EntityBase other)
    {
        return team != other.team;
    }
}
