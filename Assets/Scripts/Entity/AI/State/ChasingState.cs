using UnityEngine;

public class ChasingState : BaseState
{
    public ChasingState(AIContext context) : base(context)
    {
    }
    public override void OnEnter()
    {
        Context.Controller.MoveToOrigin();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        Context.Controller.MoveTo(Context.Target.position);

        float distance = Vector3.Distance(Context.Controller.transform.position, Context.Target.position);

        if (!FindTarget())
        {
            Context.Fsm.ChangeState(StateType.Wander);
        }
        if (FindTargetForAttack())
        {
            Context.Fsm.ChangeState(StateType.Attack);
        }
    }

    bool FindTarget()
    {
        if (Context.Target && Context.Controller.Stat.TryGetStat(StatType.ChasingTargetDetectRange,out var data))
        {
            return Vector3.Distance(Context.Controller.transform.position, Context.Target.position) < data.FinalValue;
        }
        return false;
    }

    bool FindTargetForAttack()
    {
        if (Context.Target && Context.Controller.Stat.TryGetStat(StatType.AttackRange, out var data))
        {
            return Vector3.Distance(Context.Controller.transform.position, Context.Target.position) < data.FinalValue;
        }
        return false;
    }
}