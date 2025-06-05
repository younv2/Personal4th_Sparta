using UnityEngine;

public class DeadState : BaseState
{
    public DeadState(AIContext context) : base(context)
    {
    }
    public override void OnEnter()
    {
        Context.Controller.StopMoving();
        Context.Controller.PlayDieAnimation();

    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
    }
}