using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(NavMeshAgent))]
public class Minion : MonoBehaviour
{
    protected int Life { get; set; }
    protected int HitRate { get; set; }
    protected int Damage { get; set; }
    protected int Speed { get; set; }
    private bool canFly = false;
    public bool isEnemy = false;
    public bool isAttacking = false;

    internal bool hasBeenInitialized = false;

    private NavMeshAgent agent;
    [SerializeField] public Transform target;

    internal void Initialize()
    {
        if(hasBeenInitialized) return; 
        agent = GetComponent<NavMeshAgent>();
        SetDestination();
        hasBeenInitialized = true;
    }
    protected void SetDestination()
    {
        if(target == null)
        {
            Building[] buildings = FindObjectsOfType<Building>();
            foreach (var building in buildings)
            {
                if(isEnemy != building.isEnemy && building.gameObject.CompareTag("Base")) 
                {
                    target = building.gameObject.transform;
                    break;
                }
            }
        }
        agent.SetDestination(target.position);
    }

    void AttackingBuilding()
    {
        isAttacking = true;
        /*
            C'est le minion qui inflige les dega au batiment
            si distanceRemaining est suffisante
            alors il attack le building
        */
    }
}
