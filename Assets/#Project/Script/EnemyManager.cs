using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //il manque la base
    [Header("Tower Settings")]

    [SerializeField] private List<GameObject> enemyTowers;
    [SerializeField] private int scoreToGetTower;
    [SerializeField] private int towerWoodCost = 1;
    [SerializeField] private int towerStoneCost = 1;
    [SerializeField] private int towerLife = 1;
    [SerializeField] private int towerScoreReward = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageRate = 1.0f; 
    [SerializeField] public static bool enemyTowerEnable = false;
    private List<Building> allEnemyBuildings;

    [Header("Forge Settings")]
    [SerializeField] private GameObject enemyForge;
    [SerializeField] private int scoreToGetForge;
    [SerializeField] private int forgeWoodCost = 1;
    [SerializeField] private int forgeStoneCost = 1;
    [SerializeField] private int forgeLife = 1;
    [SerializeField] private int forgeScoreReward = 1; 
    [SerializeField] public float forgeMinionRate = 1.0f;
    [SerializeField] public static bool enemyForgeEnable = false;

    [Header("Gym Settings")]
    [SerializeField] private GameObject enemyGym;
    [SerializeField] private int scoreToGetGym;
    [SerializeField] private int gymWoodCost = 1;
    [SerializeField] private int gymStoneCost = 1;
    [SerializeField] private int gymLife = 1;
    [SerializeField] private int gymScoreReward = 1;
    [SerializeField] public float gymMinionRate = 1.0f;
    [SerializeField] public static bool enemyGymEnable = false;
    
    [Header("Lab Settings")]
    [SerializeField] private GameObject enemyLab;
    [SerializeField] private int scoreToGetLab;
    [SerializeField] private int labWoodCost = 1;
    [SerializeField] private int labStoneCost = 1;
    [SerializeField] private int labLife = 1;
    [SerializeField] private int labScoreReward = 1; 
    [SerializeField] public float labMinionRate = 1.0f;
    [SerializeField] public static bool enemyLabEnable = false;


    void Start
    ()
    {
        GameObject enemyBuildings = GameObject.Find("Enemy Building");
        allEnemyBuildings = new List<Building>(enemyBuildings.GetComponentsInChildren<Building>());

        foreach (Building building in allEnemyBuildings)
        {
            if (building.Type != BuildingType.BASE) building.gameObject.SetActive(false);
            Debug.Log(building.name);
            if(building.Type == BuildingType.FORGE) enemyForge = building.gameObject;
            if(building.Type == BuildingType.GYM) enemyGym = building.gameObject;
            if(building.Type == BuildingType.LAB) enemyLab = building.gameObject;
            if(building.Type == BuildingType.TOWER) enemyTowers.Add(building.gameObject);
        }
    }
    public void SetUpBuildingAssets(Building building)
    {
        //if(type == BuildingType.FORGE)
    }

    private void OnEnable()
    {
        // Subscribe to the event
        PlayerData.OnScoreReachedEnemy += EnemyManagerScoreEvent;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event to avoid memory leaks
        PlayerData.OnScoreReachedEnemy -= EnemyManagerScoreEvent;
    }
    public void EnemyManagerScoreEvent()
    {
        if(!enemyTowerEnable && PlayerData.score > scoreToGetTower)
        {
            enemyTowerEnable = true;
            enemyTowers[0].SetActive(true);
        }
        else if (!enemyForgeEnable && PlayerData.score > scoreToGetForge)
        {
            enemyForgeEnable = true;
            enemyForge.SetActive(true);
        }
        else if (!enemyGymEnable && PlayerData.score > scoreToGetGym)
        {
            enemyGymEnable = true;
            enemyGym.SetActive(true);
        }
        else if (!enemyLabEnable && PlayerData.score > scoreToGetForge)
        {
            enemyLabEnable = true;
            enemyLab.SetActive(true);
        }
    }
    public static void EnemyManagerDestroyedEvent(){}

}
