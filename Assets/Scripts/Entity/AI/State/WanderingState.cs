using UnityEngine;

public class WanderingState : BaseState
{
    private float idleChance = 0.5f;
    public WanderingState(AIContext context) : base(context)
    {
    }
    public override void OnEnter()
    {
        Context.Controller.StopMoving();
        Context.Controller.MoveToRandom();
    }

    public override void OnUpdate()
    {
        if (Context.Controller.IsStopped)
        {
            if(idleChance> Random.value)
                Context.Fsm.ChangeState(StateType.Idle);
            else
                Context.Controller.MoveToRandom();
        }
        if (FindTarget())
            Context.Fsm.ChangeState(StateType.Chase);
    }

    public override void OnExit()
    {
        Context.Controller.StopMoving();
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