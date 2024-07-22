using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPoolMember : Minion
{
    public MinionPool Pool;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void HandleDeath()
    {
        Debug.Log("MINION DESTROYED");
        if(isEnemy)
        { 
            PlayerData.IncreaseScore(ScoreReward);
            Debug.Log($"Player score: {PlayerData.score}");
        }
        Pool.Kill(this);
    }
}
