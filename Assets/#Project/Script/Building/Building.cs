using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        }
        else
        {
            PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();
            playerManager.SetUpBuildingAssets(this); 
            //Debug.Log($"Building: {gameObject.name},Life {Life}, MinionRate: {GetComponentInChildren<MinionFactory>().Cooldown}, Score Reward {ScoreReward}  ");
        }
    }
    public virtual void  TakeDamage(int damage)
    {
        if(gameObject.CompareTag("Tower")) 
        {
            HitMaker hitMaker = GetComponentInChildren<HitMaker>();
            hitMaker.CreateHit(gameObject);
        }
        if(Life >= 0)
        {
            Life -= damage;
        }
        else
        {
            if(gameObject.CompareTag("Base"))
            {
                Debug.Log("END OF GAME");
                SceneManager.LoadScene("SampleScene");
                PlayerData.score = 0;
                PlayerData.wood = 0;
                PlayerData.score = 0;
            }
            PlayerData.IncreaseScore(ScoreReward);
            Destroy(gameObject);
        }
    }
}
