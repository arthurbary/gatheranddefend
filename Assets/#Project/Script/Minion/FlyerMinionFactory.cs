/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerMinionFactory : MinionFactory
{
    [SerializeField] float cooldown = 1.0f;
    [SerializeField] GameObject prefab;
    [SerializeField]private HeavyMinionPool pool;
    [SerializeField] private Transform launchPoint;
    void Start()
    {
        Cooldown = cooldown;
        Prefab = prefab;
        base.pool = pool;
        LaunchPoint = launchPoint;
        Initialize();
    }
}
 */