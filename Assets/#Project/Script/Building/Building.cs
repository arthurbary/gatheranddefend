using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public enum BuildingType
    {
        BASE = 1,
        BLACKSMITH = 2,
        GYM = 3,
        LAB = 4,
        TOWER = 5

    }
    public int WoodCost { get; protected set; }
    public int StoneCost { get; protected set; }
    protected int Level { get; set; }
    public int Life { get; set; }
    public BuildingType Type { get; set; }
    public int ScoreReward { get; set; }
    public bool isEnemy = false;
    public bool isCreated = false;

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
                PlayerData.IncreaseScore(ScoreReward);
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
