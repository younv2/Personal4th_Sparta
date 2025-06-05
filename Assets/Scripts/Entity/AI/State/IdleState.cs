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
        if (Context.Target == null) return false;

        BaseAIController controller = Context.Controller;
        Vector3 origin = controller.transform.position + Vector3.up * 0.5f;
        Vector3 direction = (Context.Target.position - origin);
        float distance = direction.magnitude;

        //if (distance > controller.Stat.TargetDetectedRange)
            return false;

        return Physics.Raycast(origin, direction.normalized, out var hit, distance) &&
               hit.transform == Context.Target;
    }
}