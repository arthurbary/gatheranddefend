using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : Building
{
    [SerializeField] private int life = 1;
    [SerializeField] private int scoreReward = 1;

    protected override void Awake()
    {
        Type = BuildingType.BASE;
        base.Awake();
    }
}
