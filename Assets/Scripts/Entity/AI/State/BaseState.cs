public abstract class BaseState
{
    protected AIContext Context { get; }

    public BaseState(AIContext context)
    {
        Context = context;
    }
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    public void OnDead() 
    {
        Context.Fsm.ChangeState(StateType.Dead);
    }
}
