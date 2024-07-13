using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerMinionPoolMember : Minion
{
    public FlyerMinionPool pool;

    [SerializeField] private int life;
    [SerializeField] private int damage;
    [SerializeField] private float damageRate;

    void Start()
    {
        Life = (life != 0) ? life : 1;
        Damage = (damage != 0) ? damage : 2;
        DamageRate = (damageRate != 0) ? damageRate : 1;
        Type = MinionType.HEAVY;
    }
    private void OnBecameInvisible(){
        //pool.Kill(this);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void HandleDeath()
    {
        Debug.Log("MINION DESTROYED");
        pool.Kill(this);
    }
}
