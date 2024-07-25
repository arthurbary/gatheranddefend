using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab : Building
{
    protected override void Awake()
    {
        Type = BuildingType.LAB;
        base.Awake();
    }
}
