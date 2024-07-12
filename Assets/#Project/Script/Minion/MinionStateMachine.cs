using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionStateMachine : MonoBehaviour
{
    public IState CurrentState {get; private set;}
    public MinionAttackState attackState;
    public MinionChaseState chaseState;
    public MinionStateMachine(NavMeshAgent agent, Transform playerTransform, Building enemy)
    {
        attackState = new MinionAttackState(enemy, this);
    }
    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }
    public void Update()
    {
        CurrentState?.Update();
    }
    public void TansitionTo(IState nextState)
    {
        CurrentState?.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }
}
