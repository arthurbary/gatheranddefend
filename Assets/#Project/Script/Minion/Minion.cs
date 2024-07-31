using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

public enum MinionType
{
    REGULAR = 1,
    HEAVY = 2,
    RUNNER = 3,
    FLYER = 4

}
public enum MinionState
{
    Initialize,
    Attacking,
    Walk,
    Damaged,
    Destroy
}
[RequireComponent(typeof(NavMeshAgent))]
public class Minion : MonoBehaviour
{
    [SerializeField]public int Life { get; set; }
    [SerializeField]public int Damage { get; set; }
    [SerializeField]public float DamageRate { get; set; }
    public MinionType Type { get; set; }
    public int ScoreReward { get; set; }
    public bool isEnemy = false;
    public bool canFly = false;
    internal bool hasBeenInitialized = false;
    private bool isAttacking = false;
    public bool isTarget = false;
    public TowerStateMachine attackingTower = null;
    private NavMeshAgent agent;
    //VARIABLE POUR SET LA TARGET
    [HideInInspector] public float baseRate;
    [HideInInspector] public float towerRate;
    [HideInInspector] public float otherBuildingRate;
    public Transform target;
    public MinionState state;
    protected virtual void Awake()
    {
        baseRate = MinionManager.BaseRate;
        towerRate = MinionManager.TowerRate;
        otherBuildingRate = MinionManager.OtherBuildingRate;
    }
    internal void Initialize()
    {
        if(hasBeenInitialized) return; 
        agent = GetComponent<NavMeshAgent>();
        setTarget();
        hasBeenInitialized = true;
        if(isEnemy)
        {
            EnemyManager enemyManager = GameObject.FindObjectOfType<EnemyManager>();
            enemyManager.SetUpMinionAssets(this);
        }
        else
        {
            PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();
            playerManager.SetUpMinionAssets(this);
            Debug.Log($"Minion Type: {Type}, Life: {Life}, Speed: {GetComponent<NavMeshAgent>().speed}, Damage: {Damage}, Damage Rate: {DamageRate}, Score Reward {ScoreReward}"); 
        }
        state = MinionState.Walk;
    }

    void Update()
    {
        switch(state){
            case MinionState.Initialize: Initialize(); break;
            case MinionState.Walk: Walk(); break;
        }
     
    }
    void Walk()
    {
        if(target != null)
        {
            if ( gameObject.activeSelf && agent.remainingDistance <= 3.0f && !isAttacking && !agent.pathPending)
            {
                StartCoroutine(Attack());
                
            }
        } 
        else
        {
            setTarget();

        }
    }
    
    

    public IEnumerator Attack()
    {

        state = MinionState.Attacking;
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
        }
        Debug.Log($"Minion attack rate: {DamageRate}");
        yield return new WaitForSeconds(DamageRate);
        isAttacking = false;
        agent.isStopped = false;
        state = MinionState.Walk;
    }

    public virtual void TakeDamage(int damage, GameObject minionHited)
    {
        HitMaker hitMaker = GetComponentInChildren<HitMaker>();
        hitMaker.CreateHit(gameObject);
        if (Life - damage > 0)
        {
            Debug.Log($"Minion Type {Type} Take {damage} Damage");
            state = MinionState.Damaged;
            Life -= damage;
            //attendre pour l'animation de se faire ?
            state = MinionState.Walk;
        }
        else
        {
            target = null;
            state = MinionState.Destroy;
            HandleDeath();
        }
    }
    protected virtual void HandleDeath(){}

    //TO test only on the base
    protected void SetTargetBuildingType()
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
                if (isEnemy != building.isEnemy && building.isCreated && building.gameObject.activeSelf)
                {
                    //buildingsEnemy.Add(building);
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

            //Add Player as target?
            
            // Determine target based on probabilities
            float rand = Random.Range(0f, 1f);
            if ( baseRate > rand && baseBuildings.Count > 0)
            {
                target = baseBuildings[Random.Range(0, baseBuildings.Count)].gameObject.transform;
            }
            else if (baseRate + towerRate > rand && towerBuildings.Count > 0)
            {
                target = towerBuildings[Random.Range(0, towerBuildings.Count)].gameObject.transform;
            }
            else if (baseRate + towerRate + otherBuildingRate >rand && otherBuildings.Count > 0)
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

            if (target != null && target.gameObject.activeSelf)
            {
                agent.SetDestination(target.position);
                agent.isStopped = false;
            }
            else if( (target == null || !target.gameObject.activeSelf) &&  (baseBuildings.Count > 0 || towerBuildings.Count > 0 || otherBuildings.Count > 0 || resources.Count > 0 || buildingsEnemy.Count > 0))
            {
                setTarget();
            }
        }
    }
}
