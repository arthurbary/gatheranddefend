using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBasic : Weapon
{
    [SerializeField] private int damage;
    [SerializeField] private float damageRate;

    void Start()
    {
        Damage = (damage != 0) ? damage : 1;
        DamageRate = (damageRate != 0) ? damageRate : 1;
    }
}
