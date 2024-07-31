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
    UnityEngine.UI.Image image;
    protected virtual void Awake()
    {
        baseUnderAtack = GameObject.FindWithTag("BaseUnderAttack");
        image = baseUnderAtack.GetComponent<UnityEngine.UI.Image>();
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
    protected virtual void Update()
    {
        Debug.Log("UPDATE BUILDING");
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
    void BaseUnderAttack()
    {
        //var image = baseUnderAtack.GetComponent<UnityEngine.UI.Image>();
        var color = image.color;
        color.a = 0.3f; // Change the alpha value
        image.color = color; // Set the modified color back to the image component
    }

    void ReduceHitScreen()
    {
        Debug.Log($"Image:{image != null}");
        if(image != null)
        {
            var color = image.color;
            color.a -= 0.01f;
            image.color = color;
        }
    }
}
