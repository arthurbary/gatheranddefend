using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Minion : MonoBehaviour
{
    protected int Life { get; set; }
    protected int HitRate { get; set; }
    protected int Damage { get; set; }
    protected int Speed { get; set; }
    private bool canFly = false;
    public bool isEnemy = false;

    private NavMeshAgent agent;
    [SerializeField] protected Transform targets;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetDestination();
    }
    protected void SetDestination()
    {
        agent.SetDestination(targets.position);
    }
}
