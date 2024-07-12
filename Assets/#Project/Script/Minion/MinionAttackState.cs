using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttackState : IState
{
    //le but est de attacker un batiment, si celui si est detruit alors choisir le prochain batiment
    public MinionAttackState(Building enemyBuilding, MinionStateMachine stateMachine)
    {
    }
}
