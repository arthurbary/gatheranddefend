using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPoolMember : Minion
{
    public MinionPool pool;
    private DisplayManager displayManager;
    public delegate void TargetHandler();
    public static event TargetHandler OnScoreReached;
    public static event TargetHandler OnScoreReachedEnemy;

    void Start()
    {
        displayManager = GameObject.FindObjectOfType<DisplayManager>();
    }

    protected override void HandleDeath()
    {
        Debug.Log("MINION DESTROYED");
        if(isEnemy  && gameObject.activeSelf) PlayerData.IncreaseScore(ScoreReward);
        if(attackingTower != null) 
        {
            attackingTower.minionToKill.Remove(this);
            attackingTower = null;
        }
        pool.Kill(this);

    }
}
