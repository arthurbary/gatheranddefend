using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyMinionPool : MinionPool
{
    private Stack<HeavyMinionPoolMember> pool = new();
    [Range(1, 100)][SerializeField] private int initialBatch = 50;
    [Range(1, 100)][SerializeField] private int batch = 10;

    [SerializeField] GameObject prefab;

    void Awake()
    {
        
    }
}
