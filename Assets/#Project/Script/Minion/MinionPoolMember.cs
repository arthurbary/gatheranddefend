using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionPoolMember : Minion
{
    public MinionPool pool;
    [SerializeField] private DisplayManager displayManager;


    protected override void HandleDeath()
    {
        Debug.Log("MINION DESTROYED");
        if(isEnemy  && gameObject.activeSelf)
        {
            displayManager = GameObject.FindObjectOfType<DisplayManager>();
            PlayerData.IncreaseScore(ScoreReward);
            displayManager.UpdatePlayerBoard();
        }
        if(attackingTower != null) 
        {
            attackingTower.minionToKill.Remove(this);
            attackingTower = null;
        }
        pool.Kill(this);

    }
}
