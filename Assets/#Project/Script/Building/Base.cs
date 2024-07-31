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
    protected override void Update()
    {
        base.Update();
        if(lastLife != Life)
        {
            Debug.Log($" Base name: {gameObject.name} Life: {Life}");
            lastLife = Life;
        }
    }
}
