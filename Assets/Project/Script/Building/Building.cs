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
        TOWER = 5,
        ROAD = 6

    }
    protected int WoodCost { get; set; }
    protected int StoneCost { get; set; }
    protected int Level { get; set; }
    protected int Life { get; set; }
    protected BuildingType Type { get; set; }
    public bool isEnemy = false;

    public void TakeDamage(int damage)
    {
        if(Life >= 0)
        {
            Life -= damage;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
