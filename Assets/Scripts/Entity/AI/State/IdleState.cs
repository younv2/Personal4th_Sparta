using UnityEngine;

public class IdleState : BaseState
{
    private float wanderChance = 0.005f;

    public IdleState(AIContext context) : base(context)
    {
    }

    public override void OnEnter()
    {
        Context.Controller.StopMoving();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        if(Random.value < wanderChance)
        {
            Context.Fsm.ChangeState(StateType.Wander);
        }
        if (FindTarget())
            Context.Fsm.ChangeState(StateType.Chase);
    }

    bool FindTarget()
    {
        if (Context.Target && Context.Controller.Stat.TryGetStat(StatType.WanderTargetDetectRange, out var data))
        {
            return Vector3.Distance(Context.Controller.transform.position, Context.Target.position) < data.FinalValue;
        }
        return false;
    }
}