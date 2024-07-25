using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Tower : Building
{
    protected override void Awake()
    {
        Type = BuildingType.TOWER;
        base.Awake();
    }
}
