using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPoolMember : Minion
{
    public MinionPool Pool;
    private DisplayManager displayManager;

    void Start()
    {
        displayManager = GameObject.FindObjectOfType<DisplayManager>();
    }
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
        }
        Pool.Kill(this);
    }
}
