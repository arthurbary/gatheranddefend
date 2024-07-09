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
    protected int woodCost { get; set; }
    protected int stoneCost { get; set; }
    protected int level { get; set; }
    protected int life { get; set; }
    
    public abstract void GenerateMinion();
}
