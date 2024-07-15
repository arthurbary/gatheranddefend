using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPoolMember : Minion
{
    public MinionPool Pool;
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
        Pool.Kill(this);
    }
}
