/* using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeavyMinionFactory : MinionFactory
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