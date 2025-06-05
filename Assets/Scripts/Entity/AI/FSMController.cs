using System.Collections.Generic;
using UnityEngine;

public class FSMController : MonoBehaviour
{
    [SerializeField] private StateSetSO stateSet;
    private Dictionary<StateType, BaseState> stateDic = new();
    private BaseState currentState;
    AIContext context;

    private void Start()
    {
        context = new AIContext(GetComponent<BaseAIController>(),this);
        foreach (var type in stateSet.stateTypes)
            stateDic[type] = CreateState(type);
        ChangeState(StateType.Wander);
    }
    public void HandleDeath()
    {
        currentState.OnDead();
    }
    private void Update()
    {
        currentState?.OnUpdate();
    }
    public void ChangeState(StateType state)
    {
        if (currentState != stateDic[state])
        {
            currentState?.OnExit();
            currentState = stateDic[state];
            currentState?.OnEnter();
        }
    }
    BaseState CreateState(StateType t)
    {
        return t switch
        {
            StateType.Idle => new IdleState(context),
            StateType.Wander => new WanderingState(context),
            StateType.Chase => new ChasingState(context),
            StateType.Attack => new AttackState(context),
            StateType.Dead => new DeadState(context),
            _ => null
        };
    }

}
public class AIContext
{
    public AIContext(BaseAIController controller, FSMController fsm)
    {
        Controller = controller;
        Fsm = fsm;
    }
    public BaseAIController Controller { get; }
    public FSMController Fsm { get; }
    public Transform Target => Controller.CurrentTarget?.transform;
}