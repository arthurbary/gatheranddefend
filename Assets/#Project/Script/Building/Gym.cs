using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym : Building
{
    [SerializeField] private int woodCost = 1;
    [SerializeField] private int stoneCost = 1;
    [SerializeField] private int life = 1;
    [SerializeField] private int scoreReward = 1;
    void Start()
    {
        WoodCost = woodCost;
        StoneCost = stoneCost;
        Life = life;
        ScoreReward = scoreReward;
        Type = BuildingType.GYM;
    }
}
