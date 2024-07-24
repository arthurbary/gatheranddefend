using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int scoreForEnemyTower;
    [SerializeField] private int scoreForEnemyForge;
    [SerializeField] private int scoreForEnemyGym;
    [SerializeField] private int scoreForEnemyLab;
    [SerializeField] public static bool enemyTowerEnable = false;
    [SerializeField] public static bool enemyForgeEnable = false;
    [SerializeField] public static bool enemyGymEnable = false;
    [SerializeField] public static bool enemyLabEnable = false;
    [SerializeField] private GameObject enemyTower;
    [SerializeField] private GameObject enemyForge;
    [SerializeField] private GameObject enemyGym;
    [SerializeField] private GameObject enemyLab;
    void Awake()
    {

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
        if(!enemyTowerEnable && PlayerData.score > scoreForEnemyTower)
        {
            enemyTowerEnable = true;
            enemyTower.SetActive(true);
        }
        else if (!enemyForgeEnable && PlayerData.score > scoreForEnemyForge)
        {
            enemyForgeEnable = true;
        }
        else if (!enemyGymEnable && PlayerData.score > scoreForEnemyGym)
        {
            enemyGymEnable = true;
        }
        else if (!enemyLabEnable && PlayerData.score > scoreForEnemyLab)
        {
            enemyLabEnable = true;
        }
    }
    public static void EnemyManagerDestroyedEvent(){}

}
