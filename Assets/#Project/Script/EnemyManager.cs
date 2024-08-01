using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Manager
{

    [Header("Enemy Buildings Settings")]
    [SerializeField] private GameObject enemyBase;
    [SerializeField] private List<GameObject> enemyTowers;
    [SerializeField] private GameObject enemyForge;
    [SerializeField] private GameObject enemyGym;
    [SerializeField] private GameObject enemyLab;
    private List<Building> allEnemyBuildings;
    private List<GameObject> allEnemyTowers = new List<GameObject>();

    void Start()
    {
        GameObject enemyBuildings = GameObject.Find("Enemy Building");
        allEnemyBuildings = new List<Building>(enemyBuildings.GetComponentsInChildren<Building>());

        foreach (Building building in allEnemyBuildings)
        {
            if(building.Type != BuildingType.BASE) building.gameObject.SetActive(false);
            if(building.Type == BuildingType.BASE) enemyBase = building.gameObject;
            if(building.Type == BuildingType.FORGE) enemyForge = building.gameObject;
            if(building.Type == BuildingType.GYM) enemyGym = building.gameObject;
            if(building.Type == BuildingType.LAB) enemyLab = building.gameObject;
            if(building.Type == BuildingType.TOWER) allEnemyTowers.Add(building.gameObject);
        }
        //Enemy start with one tower
        allEnemyTowers[0].SetActive(true);
    }
    
    private void OnEnable()
    {
        // Subscribe to the event
        PlayerData.OnScoreReachedEnemy += EnemyManagerScoreEvent;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        PlayerData.OnScoreReachedEnemy -= EnemyManagerScoreEvent;
    }
    public void EnemyManagerScoreEvent()
    {
        if(!towerEnable && PlayerData.score > scoreToGetTower)
        {
            towerEnable = true;
            StartCoroutine(LaunchTowerManager());
        }
        else if (!forgeEnable && PlayerData.score > scoreToGetForge)
        {
            forgeEnable = true;
            enemyForge.SetActive(true);
            enemyForge.GetComponentInChildren<MinionFactory>().CanLaunchMinion = true;
            enemyForge.GetComponentInChildren<MinionFactory>().Initialize();
        }
        else if (!gymEnable && PlayerData.score > scoreToGetGym)
        {
            gymEnable = true;
            enemyGym.SetActive(true);
            enemyGym.GetComponentInChildren<MinionFactory>().CanLaunchMinion = true;
            enemyGym.GetComponentInChildren<MinionFactory>().Initialize();
            
        }
        else if (!labEnable && PlayerData.score > scoreToGetForge)
        {
            labEnable = true;
            enemyLab.SetActive(true);
            enemyLab.GetComponentInChildren<MinionFactory>().CanLaunchMinion = true;
            enemyLab.GetComponentInChildren<MinionFactory>().Initialize();
        }
    }
    public static void EnemyManagerDestroyedEvent(){}

    private IEnumerator LaunchTowerManager()
    {
        foreach (GameObject tower in allEnemyTowers)
        {
            if(!tower.activeSelf) tower.SetActive(true);
            yield return new WaitForSeconds(30f);
        }
    }
}
