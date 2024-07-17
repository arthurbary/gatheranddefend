using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerMinionPool : MinionPool
{
    [Range(1, 100)][SerializeField] private int initialBatch = 50;
    [Range(1, 100)][SerializeField] private int batch = 10;

    [SerializeField] GameObject prefab;

    new void Awake()
    {
        InitialBatch = initialBatch;
        Batch = batch;
        Prefab = prefab;
        base.Awake();
    }
}
