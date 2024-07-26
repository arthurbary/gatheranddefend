using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMinionPoolMember : MinionPoolMember
{
    

    [SerializeField] private int life;
    [SerializeField] private int damage;
    [SerializeField] private float damageRate;
    [SerializeField] private int scoreReward;

    protected override void Awake()
    {
        Type = MinionType.REGULAR;
        base.Awake();
        
    }
}
