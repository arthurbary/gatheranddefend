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
    protected float DamageRate { get; set; }
    public MinionType Type { get; set; }
    public int ScoreReward { get; set; }
    public bool isEnemy = false;
    internal bool hasBeenInitialized = false;
    private bool isAttacking = false;
    private NavMeshAgent agent;
    //VARIABLE POUR SET LA TARGET
    [SerializeField] private float baseRate = 0.5f;
    [SerializeField] private float towerRate = 0.3f;
    [SerializeField] private float otherBuildingRate = 0.1f;
    [SerializeField] public Transform target;

    internal void Initialize()
    {
        if(hasBeenInitialized) return; 
        agent = GetComponent<NavMeshAgent>();
        setTarget();
        hasBeenInitialized = true;
    }

    void Update()
    {
        if(target != null)
        {
            if ( gameObject.activeSelf && agent.remainingDistance <= 3.0f && !isAttacking)
            {
                StartCoroutine(Attack());
            }
        } else
        {
            setTarget();
        }
    }
    

    public IEnumerator Attack()
    {

        isAttacking = true;
        agent.isStopped = true;
        if (target != null)
        {  
            if(target.GetComponent<Building>() != null && target.GetComponent<Building>().isCreated)
            {
                Building targetBuilding = target.GetComponent<Building>();
                targetBuilding.TakeDamage(Damage);
            }
            else if(target.GetComponent<Resource>() != null && target.GetComponent<Resource>().isCreated)
            {
                Resource targetResource = target.GetComponent<Resource>();
                targetResource.TakeDamage(Damage);
            }
        }
        else
        {
            Debug.Log("TARGET IS DESTROYED");
            setTarget();
        }
        yield return new WaitForSeconds(DamageRate);
        isAttacking = false;
    }

    public virtual void TakeDamage(int damage)
    {
        if (Life - damage > 0)
        {
            Debug.Log("Minion Take Damage");
            Life -= damage;
        }
        else
        {
            target = null;
            HandleDeath();
        }
    }

    protected virtual void HandleDeath(){}

    //TO test only on the base
    protected void SetTargetBase()
    {
        if(target == null)
        {
            Building[] buildings = FindObjectsOfType<Building>();
            foreach (var building in buildings)
            {
                if(isEnemy != building.isEnemy && building.gameObject.CompareTag("Gym")) 
                {
                    target = building.gameObject.transform;
                    break;
                }
            }
        }
        if(target != null) agent.SetDestination(target.position);
    }
    //TO test on random object
    void setTargetRandom()
    {
        if(target == null)
        {
            //Buildings Targeting
            Building[] buildings = FindObjectsOfType<Building>();
            List<Building> buildingsEnemy = new List<Building>();
            foreach (var building in buildings)
            {
                if(isEnemy != building.isEnemy)buildingsEnemy.Add(building);
            }
            if(buildingsEnemy.Count > 0)
            {
                target = buildingsEnemy[Random.Range(0,buildingsEnemy.Count)].gameObject.transform;
            }
        }
        agent.SetDestination(target.position);
    }

    void setTarget()
    {
        /* 
        Reflexion 
        s'arranger pour qu'il ne 'attaque pas deux fois de suite le meme type de target
        Definir si c'est l'ordre d'importance Base > Tower > Resource > other
        */
        if (target == null)
        {
            // Buildings Targeting
            Building[] buildings = FindObjectsOfType<Building>();
            List<Building> baseBuildings = new List<Building>();
            List<Building> towerBuildings = new List<Building>();
            List<Building> otherBuildings = new List<Building>();
            List<Building> buildingsEnemy = new List<Building>();

            foreach (var building in buildings)
            {
                if (isEnemy != building.isEnemy && building.isCreated)
                {
                    buildingsEnemy.Add(building);
                    if (building.tag == "Base")
                    {
                        baseBuildings.Add(building);
                    }
                    else if (building.tag == "Tower")
                    {
                        towerBuildings.Add(building);
                    }
                    else if(building.tag != "Base" && building.tag != "Tower")
                    {
                        otherBuildings.Add(building);
                    }
                }
            }

            // Resources Targeting Only Enemy can destroy ressources
            List<Resource> resources = new List<Resource>();
            if(isEnemy)
            {
                foreach (var resource in FindObjectsOfType<Resource>())
                {
                    if(resource.isCreated)resources.Add(resource);
                }
            }

            //Add Player as target
            
            // Determine target based on probabilities
            float rand = Random.Range(0f, 1f);
            if (rand < baseRate && baseBuildings.Count > 0)
            {
                target = baseBuildings[Random.Range(0, baseBuildings.Count)].gameObject.transform;
            }
            else if (rand < baseRate + towerRate && towerBuildings.Count > 0)
            {
                target = towerBuildings[Random.Range(0, towerBuildings.Count)].gameObject.transform;
            }
            else if (rand < baseRate + towerRate + otherBuildingRate && otherBuildings.Count > 0)
            {
                target = otherBuildings[Random.Range(0, otherBuildings.Count)].gameObject.transform;
            }
            else if (resources.Count > 0)
            {
                target = resources[Random.Range(0, resources.Count)].gameObject.transform;
            }
            else if (buildingsEnemy.Count > 0) // Fallback to any enemy building if specific categories are empty
            {
                target = buildingsEnemy[Random.Range(0, buildingsEnemy.Count)].gameObject.transform;
            }

            if (target != null)
            {
                agent.SetDestination(target.position);
                agent.isStopped = false;
            }
            else if( target == null &&  (baseBuildings.Count > 0 || towerBuildings.Count > 0 || otherBuildings.Count > 0 || resources.Count > 0 || buildingsEnemy.Count > 0))
            {
                setTarget();
            }
        }
    }
}
