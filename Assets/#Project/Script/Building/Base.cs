using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : Building
{
    [SerializeField] private int life = 1;
    [SerializeField] private int scoreReward = 1;

    void Awake()
    {
        Life = life;
        ScoreReward = scoreReward;
        Type = BuildingType.BASE;
    }
}
