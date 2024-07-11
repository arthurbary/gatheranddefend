using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMinionPoolMember : Minion
{
    
    public RegularMinionPool pool;

    [SerializeField] private int life;
    [SerializeField] private int damage;

    void Start()
    {
        Life = (life != 0) ? life : 1;
        Damage = (damage != 0) ? damage : 1;
        Type = MinionType.REGULAR;
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
