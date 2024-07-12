using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : Building
{
    [SerializeField] private int woodCost = 1;
    [SerializeField] private int stoneCost = 1;
    [SerializeField] private int life = 1;

    void Start()
    {
        WoodCost = woodCost;
        StoneCost = stoneCost;
        Life = life;
        Type = BuildingType.BASE;
    }
}
