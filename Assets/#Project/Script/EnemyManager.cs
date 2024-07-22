using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public static int scoreForEnemyTower;
    [SerializeField] public static int scoreForEnemyForge;
    [SerializeField] public static int scoreForEnemyGym;
    [SerializeField] public static int scoreForEnemyLab;
    [SerializeField] public static bool enemyTowerEnable = false;
    [SerializeField] public static bool enemyForgeEnable = false;
    [SerializeField] public static bool enemyGymEnable = false;
    [SerializeField] public static bool enemyLabEnable = false;

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
    public static void EnemyManagerScoreEvent()
    {
        if(!enemyTowerEnable && PlayerData.score > scoreForEnemyTower)
        {
            enemyTowerEnable = true;
            // activation du button
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
