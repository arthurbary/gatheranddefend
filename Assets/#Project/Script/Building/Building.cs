using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private GameObject baseUnderAtack;
    EndGameManager endGameManager;
    UnityEngine.UI.Image image;
    protected virtual void Awake()
    {
        endGameManager = FindObjectOfType<EndGameManager>();
        baseUnderAtack = GameObject.FindWithTag("BaseUnderAttack");
        image = baseUnderAtack.GetComponent<UnityEngine.UI.Image>();
        if(isEnemy)
        {
            EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
            enemyManager.SetUpBuildingAssets(this);
        }
        else
        {
            PlayerManager playerManager = FindObjectOfType<PlayerManager>();
            playerManager.SetUpBuildingAssets(this); 
            //Debug.Log($"Building: {gameObject.name},Life {Life}, MinionRate: {GetComponentInChildren<MinionFactory>().Cooldown}, Score Reward {ScoreReward}  ");
        }
    }
    protected virtual void Update()
    {
        ReduceHitScreen();
    }
    public virtual void  TakeDamage(int damage)
    {
        if(gameObject.CompareTag("Base") && !isEnemy)
        {
            BaseUnderAttack();
        }
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
                endGameManager.OnEndGame(isEnemy);
            }
            PlayerData.IncreaseScore(ScoreReward);
            /* 
            TODO: essayer de faire une version de active et non active
            if(gameObject.CompareTag("Tower"))
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            } 
            */
            Destroy(gameObject);
        }
    }
    void BaseUnderAttack()
    {
        var color = image.color;
        color.a = 0.5f;
        image.color = color;
    }

    void ReduceHitScreen()
    {
        if(image != null)
        {
            var color = image.color;
            color.a -= 0.005f;
            image.color = color;
        }
    }
}
