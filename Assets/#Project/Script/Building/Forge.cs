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
    protected override void Awake()
    {
        Type = BuildingType.FORGE;
        base.Awake();
    }
}
