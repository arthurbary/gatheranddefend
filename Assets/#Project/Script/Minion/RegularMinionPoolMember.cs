using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMinionPoolMember : MinionPoolMember
{
    
    public RegularMinionPool pool;

    [SerializeField] private int life;
    [SerializeField] private int damage;
    [SerializeField] private float damageRate;
    [SerializeField] private int scoreReward;

    void Start()
    {
        Life = (life != 0) ? life : 1;
        Damage = (damage != 0) ? damage : 1;
        DamageRate = (damageRate != 0) ? damageRate : 0.1f;
        ScoreReward = (scoreReward != 0) ? scoreReward : 1;
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
        if(isEnemy) PlayerData.IncreaseScore(ScoreReward);
        pool.Kill(this);
    }
}
