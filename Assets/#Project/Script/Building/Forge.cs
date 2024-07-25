using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : Building
{
    /* 
    [SerializeField] private int woodCost = 1;
    [SerializeField] private int stoneCost = 1;
    [SerializeField] private int life = 1;
    [SerializeField] private int scoreReward = 1; 
    */
    void Awake()
    {
        /*
        WoodCost = woodCost;
        StoneCost = stoneCost;
        Life = life;
        ScoreReward = scoreReward; 
        */
        Type = BuildingType.FORGE;
        if(isEnemy)
        {
            EnemyManager enemyManager = GameObject.FindObjectOfType<EnemyManager>();
            Debug.Log($"Enemy Manager: {enemyManager.GetType()}");
            enemyManager.SetUpBuildingAssets(this);
        }
        else
        {
            /* 
            PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();
            playerManager.SetUpBuildingAssets(this); 
            */
        }
    }
}
