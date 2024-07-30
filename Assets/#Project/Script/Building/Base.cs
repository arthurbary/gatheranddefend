using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Base : Building
{
    /* 
    [SerializeField] private int life;
    [SerializeField] private int scoreReward; 
    */
    private int lastLife;

    protected override void Awake()
    {
        Type = BuildingType.BASE;
        base.Awake();
        lastLife = Life;
    }
    void Update()
    {
        if(lastLife != Life && !isEnemy)
        {
            Debug.Log($" Base Life: {Life}");
            lastLife = Life;
        }
    }
}
