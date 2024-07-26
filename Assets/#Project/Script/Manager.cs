using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] protected int baseLife = 1;
    [SerializeField] protected int baseScoreReward = 1; 
    [SerializeField] public float baseMinionRate = 1.0f;

    [Header("Tower Settings")]
    [SerializeField] protected int scoreToGetTower;
    [SerializeField] protected int towerWoodCost = 1;
    [SerializeField] protected int towerStoneCost = 1;
    [SerializeField] protected int towerLife = 1;
    [SerializeField] protected int towerScoreReward = 1;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float damageRate = 1.0f; 
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

    [Header("Reuglar Minion Settings")]
    [SerializeField] protected int Life;
    [SerializeField]protected int Damage ;
    [SerializeField]protected float DamageRate;

    public void SetUpBuildingAssets(Building building)
    {
        //if(type == BuildingType.FORGE)
        switch (building.Type)
        {
            case BuildingType.BASE:
                building.Life = baseLife;
                building.ScoreReward = baseScoreReward;
                building.GetComponentInChildren<MinionFactory>().Cooldown = baseMinionRate;
                break;
            case BuildingType.TOWER:
                building.WoodCost = towerWoodCost;
                building.StoneCost = towerStoneCost;
                building.Life = towerLife;
                building.ScoreReward = towerScoreReward;
                if(building.GetComponentInChildren<TowerStateMachine>() != null)
                {
                    TowerStateMachine towerStateMachine = building.GetComponentInChildren<TowerStateMachine>();
                    towerStateMachine.damage = damage;
                    towerStateMachine.damageRate = damageRate;
                    if(!building.isEnemy)
                    {
                        towerStateMachine.gameObject.SetActive(false);
                        building.GetComponentInChildren<ArrowFactory>().gameObject.SetActive(false);
                    }
                }
                else 
                {
                    Debug.Log("towerStateMachine not found");
                }
                break;
            case BuildingType.FORGE:
                //exemple pour utiliser les scriptable building.WoodCost = forgeData.forgeWoodCost;
                building.WoodCost = forgeWoodCost;
                building.StoneCost = forgeStoneCost;
                building.Life = forgeLife;
                building.ScoreReward = forgeScoreReward;
                Debug.Log($" is the factory {building.name} null ? {building.GetComponentInChildren<MinionFactory>()==null}");
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
    public void SetUpMinionAssets(Minion minion){}
}
