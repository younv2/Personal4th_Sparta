using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(AIContext context) : base(context)
    {
    }

    public override void OnEnter()
    {
        Context.Controller.StopMoving();
        if(Context.Controller is MonsterController monsterController)
        {
            monsterController.StartAttack(Context.Target);
        }
        if (Context.Controller is PlayerController playerController)
        {
            playerController.StartAttack(Context.Target);
        }
    }

    public override void OnExit()
    {
        if (Context.Controller is MonsterController monsterController)
        {
            monsterController.StopAttack();
        }
        if (Context.Controller is PlayerController playerController)
        {
            playerController.StopAttack();
        }
    }

    public override void OnUpdate()
    {
        if (!Context.Target)
        {
            Context.Fsm.ChangeState(StateType.Wander);
            return;
        }
        if (!Context.Controller.IsStopped)
        { 
            Context.Controller.StopMoving(); 
        }
        Context.Controller.LookAt(Context.Target.position);
        if (!FindTarget())
        {
            Context.Fsm.ChangeState(StateType.Chase);
        }
    }
    bool FindTarget()
    {
        if (Context.Target && Context.Controller.Stat.TryGetStat(StatType.AttackRange, out var data))
        {
            return Vector3.Distance(Context.Controller.transform.position, Context.Target.position) < data.FinalValue;
        }
        return false;
    }
}