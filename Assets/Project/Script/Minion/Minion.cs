using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(NavMeshAgent))]
public class Minion : MonoBehaviour
{
    public enum MinionType
    {
        REGULAR = 1,
        HEAVY = 2,
        RUNNER = 3,
        FLYER = 4

    }
    protected int Life { get; set; }
    protected int Damage { get; set; }
    public MinionType Type { get; set; }
    private bool canFly = false;
    public bool isEnemy = false;

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

    void Update()
    {

        StartCoroutine(Attack());
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

    public IEnumerator Attack()
    {
        /*
        TODO
        Ameliorer l'attaque il faut qu'il continue a attacker 
        tant qu'il n'est pas mort
         */
        if (agent.remainingDistance <= 5.0f && target != null)
        {  
            agent.isStopped = true;     
            Building targetBuilding = target.GetComponent<Building>();
            targetBuilding.TakeDamage(Damage);
        }
        else
        {
            Debug.Log("TARGET IS DESTROYED");
            /* 
            TODO 
            */
        }
        yield return new WaitForSeconds(0.1f);
    }
    void setTarget()
    {
        /*
            TODO
            repartir le type de building en listes
            en soit faire un randome pour choisir qu'elle liste en selon l'importance des batiments (Base, tower, ect..)
            definir la target
            la method doit etre appeler au debut et dans Attack si jamais la target est null
        */
    }

    public virtual void TakeDamage(int damage)
    {
        if (Life - damage > 0)
        {
            Life -= damage;
            Debug.Log("Life: " + Life);
            Debug.Log("Damage: " + damage);
        }
        else
        {
            HandleDeath();
        }
    }

    protected virtual void HandleDeath(){}
}
