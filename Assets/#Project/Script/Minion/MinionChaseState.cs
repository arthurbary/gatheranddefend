using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionChaseState : IState
{
    //le but est de verifier s'il est arriver a destination et de changer de state
    private NavMeshAgent agent;
    private Transform target;
    private Building enemyBuilding;
    private MinionStateMachine stateMachine;
    public MinionChaseState(Building enemyBuilding, MinionStateMachine stateMachine)
    {
        this.enemyBuilding = enemyBuilding;
        this.stateMachine = stateMachine;
        /* 
        agent = enemy.agent;
        target = enemy.target; 
        */
    }
    public void Enter() 
    {
        Debug.Log("Entering Chase State");
    }
    public void Update() 
    {
        agent.SetDestination(target.position);
        //if(!enemy.CanSeePlayer())
        //enemy remaindsitance <+ 1.5f
        if(true)
        {
            stateMachine.TansitionTo(stateMachine.attackState);
        }
    }
    public void Exit() 
    {
        Debug.Log("Exit ChaseState");
    }
}
