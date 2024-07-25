using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum BuildingType
    {
        BASE = 1,
        FORGE = 2,
        GYM = 3,
        LAB = 4,
        TOWER = 5

    }
public abstract class Building : MonoBehaviour
{
    public int WoodCost { get;  set; }
    public int StoneCost { get; set; }
    protected int Level { get; set; }
    public int Life { get; set; }
    public BuildingType Type { get; set; }
    public int ScoreReward { get; set; }
    public bool isEnemy = false;
    public bool isCreated = false;
    protected virtual void Awake()
    {
        if(isEnemy)
        {
            EnemyManager enemyManager = GameObject.FindObjectOfType<EnemyManager>();
            enemyManager.SetUpBuildingAssets(this);
            Debug.Log($"building: {gameObject.name} wood cost: {WoodCost} stone cost: {StoneCost} score reward: {ScoreReward}");
        }
        else
        {
            /* 
            PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();
            playerManager.SetUpBuildingAssets(this); 
            */
        }
    }
    public void TakeDamage(int damage)
    {
        if(Life >= 0)
        {
            Life -= damage;
        }
        else
        {
            if(isEnemy)
            {
                if(gameObject.activeSelf)PlayerData.IncreaseScore(ScoreReward);
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
