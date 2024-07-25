using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym : Building
{
    protected override void Awake()
    {
        Type = BuildingType.GYM;
        base.Awake();
    }
}
