using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBasic : Weapon
{
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;

    void Start()
    {
        Damage = (damage != 0) ? damage : 1;
        Cooldown = (cooldown != 0) ? cooldown : 0.1f;
    }
}
