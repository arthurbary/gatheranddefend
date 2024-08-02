using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] protected int baseLife = 1;
    [SerializeField] protected int baseScoreReward = 1; 
    [SerializeField] public float baseMinionRate = 1.0f;
    [SerializeField] protected int baseDamage = 1;
    [SerializeField] protected float baseDamageRate = 1.0f; 

    [Header("Tower Settings")]
    [SerializeField] protected int scoreToGetTower;
    [SerializeField] protected int towerWoodCost = 1;
    [SerializeField] protected int towerStoneCost = 1;
    [SerializeField] protected int towerLife = 1;
    [SerializeField] protected int towerScoreReward = 1;
    [SerializeField] protected int towerDamage = 1;
    [SerializeField] protected float towerDamageRate = 1.0f; 
    [SerializeField] public static bool towerEnable = false;

    [Header("Forge Settings")]
    [SerializeField] protected int scoreToGetForge;
    [SerializeField] protected int forgeWoodCost = 1;
    [SerializeField] protected int forgeStoneCost = 1;
    [SerializeField] protected int forgeLife = 1;
    [SerializeField] protected int forgeScoreReward = 1; 
    [SerializeField] public float forgeMinionRate = 1.0f;
    [SerializeField] public static bool forgeEnable = false;

    //public ForgeData forgeData;

    [Header("Gym Settings")]
    
    [SerializeField] protected int scoreToGetGym;
    [SerializeField] protected int gymWoodCost = 1;
    [SerializeField] protected int gymStoneCost = 1;
    [SerializeField] protected int gymLife = 1;
    [SerializeField] protected int gymScoreReward = 1;
    [SerializeField] public float gymMinionRate = 1.0f;
    [SerializeField] public static bool gymEnable = false;
    
    [Header("Lab Settings")]
    
    [SerializeField] protected int scoreToGetLab;
    [SerializeField] protected int labWoodCost = 1;
    [SerializeField] protected int labStoneCost = 1;
    [SerializeField] protected int labLife = 1;
    [SerializeField] protected int labScoreReward = 1; 
    [SerializeField] public float labMinionRate = 1.0f;
    [SerializeField] public static bool labEnable = false;

    [Header("Regular Minion Settings")]
    [SerializeField] protected int regularLife = 1;
    [SerializeField] protected float regularSpeed = 1;
    [SerializeField] protected int regularDamage = 1;
    [SerializeField] protected float regularDamageRate = 1;
    [SerializeField] protected int regularScoreReward = 1;
    [Header("Heavy Minion Settings")]
    [SerializeField] protected int heavyLife = 1;
    [SerializeField] protected float heavySpeed = 1;
    [SerializeField]protected int heavyDamage = 1;
    [SerializeField]protected float heavyDamageRate = 1;
    [SerializeField] protected int heavyScoreReward = 1;

    [Header("Runner Minion Settings")]
    [SerializeField] protected int runnerLife = 1;
    [SerializeField] protected float runnerSpeed = 1;
    [SerializeField]protected int runnerDamage = 1;
    [SerializeField]protected float runnerDamageRate = 1;
    [SerializeField] protected int runnerScoreReward = 1;

    [Header("Flyer Minion Settings")]
    [SerializeField] protected int flyerLife = 1;
    [SerializeField] protected float flyerSpeed = 1;
    [SerializeField]protected int flyerDamage = 1;
    [SerializeField]protected float flyerDamageRate = 1;
    [SerializeField] protected int flyerScoreReward = 1;

    void Start()
    {
        Time.timeScale = 1.5f;
    }
    public void SetUpBuildingAssets(Building building)
    {
        //if(type == BuildingType.FORGE)
        switch (building.Type)
        {
            case BuildingType.BASE:
                building.Life = baseLife;
                building.ScoreReward = baseScoreReward;
                building.GetComponentInChildren<MinionFactory>().Cooldown = baseMinionRate;
                if(building.GetComponentInChildren<TowerStateMachine>() != null)
                {
                    TowerStateMachine towerStateMachine = building.GetComponentInChildren<TowerStateMachine>();
                    towerStateMachine.damage = baseDamage;
                    towerStateMachine.damageRate = baseDamageRate;
                }
                break;
            case BuildingType.TOWER:
                building.WoodCost = towerWoodCost;
                building.StoneCost = towerStoneCost;
                building.Life = towerLife;
                building.ScoreReward = towerScoreReward;
                if(building.GetComponentInChildren<TowerStateMachine>() != null)
                {
                    TowerStateMachine towerStateMachine = building.GetComponentInChildren<TowerStateMachine>();
                    towerStateMachine.damage = towerDamage;
                    towerStateMachine.damageRate = towerDamageRate;
                    if(!building.isEnemy)
                    {
                        towerStateMachine.gameObject.SetActive(false);
                        building.GetComponentInChildren<ArrowFactory>().gameObject.SetActive(false);
                    }
                }
            
                break;
            case BuildingType.FORGE:
                //exemple pour utiliser les scriptable building.WoodCost = forgeData.forgeWoodCost;
                building.WoodCost = forgeWoodCost;
                building.StoneCost = forgeStoneCost;
                building.Life = forgeLife;
                building.ScoreReward = forgeScoreReward;
                building.GetComponentInChildren<MinionFactory>().Cooldown = forgeMinionRate;
                break;
            case BuildingType.GYM:
                building.WoodCost = gymWoodCost;
                building.StoneCost = gymStoneCost;
                building.Life = gymLife;
                building.ScoreReward = gymScoreReward;
                building.GetComponentInChildren<MinionFactory>().Cooldown = gymMinionRate;
                break;
            case BuildingType.LAB:
                building.WoodCost = labWoodCost;
                building.StoneCost = labStoneCost;
                building.Life = labLife;
                building.ScoreReward = labScoreReward;
                building.GetComponentInChildren<MinionFactory>().Cooldown = labMinionRate;
                break;
        }
    }
    public void SetUpMinionAssets(Minion minion)
    {
        switch (minion.Type)
        {
            case MinionType.REGULAR:
                minion.Life = regularLife;
                minion.Damage = regularDamage;
                minion.DamageRate = regularDamageRate;
                minion.ScoreReward = regularScoreReward;
                minion.GetComponent<NavMeshAgent>().speed = regularSpeed;
                break;
            case MinionType.HEAVY:
                minion.Life = heavyLife;
                minion.Damage = heavyDamage;
                minion.DamageRate = heavyDamageRate;
                minion.ScoreReward = heavyScoreReward;
                minion.GetComponent<NavMeshAgent>().speed = heavySpeed;
                break;
            case MinionType.RUNNER:
                minion.Life = runnerLife;
                minion.Damage = runnerDamage;
                minion.DamageRate = runnerDamageRate;
                minion.ScoreReward = runnerScoreReward;
                minion.GetComponent<NavMeshAgent>().speed = runnerSpeed;
                break;
            case MinionType.FLYER:
                minion.Life = flyerLife;
                minion.Damage = flyerDamage;
                minion.DamageRate = flyerDamageRate;
                minion.ScoreReward = flyerScoreReward;
                minion.GetComponent<NavMeshAgent>().speed = flyerSpeed;
                break;
        }
    }
}
