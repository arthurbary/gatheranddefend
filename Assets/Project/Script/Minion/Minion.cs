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
    [SerializeField] public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetDestination();
    }
    protected void SetDestination()
    {
        if(target == null)
        {
            Building[] buildings = FindObjectsOfType<Building>();
            foreach (var building in buildings)
            {
                if(isEnemy =! building.isEnemy && building.gameObject.CompareTag("Base")) 
                {
                    target = building.gameObject.transform;
                    break;
                }
            }
        }
        agent.SetDestination(target.position);
    }
}
