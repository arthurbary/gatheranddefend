using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMinionPoolMember : MinionPoolMember
{
    [SerializeField] private int life;
    [SerializeField] private int damage;
    [SerializeField] private float damageRate;

    void Start()
    {
        Life = (life != 0) ? life : 1;
        Damage = (damage != 0) ? damage : 2;
        DamageRate = (damageRate != 0) ? damageRate : 1;
        Type = MinionType.RUNNER;
    }
}
