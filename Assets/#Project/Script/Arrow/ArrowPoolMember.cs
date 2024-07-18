using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoolMember : MonoBehaviour
{
    public ArrowPool pool;
    private Minion targetMinion;
    private int damage;
    internal bool hasBeenInitialized = false;
    internal void Initialize(TowerAttackZone towerAttackZone)
    {
        damage = towerAttackZone.damage;
        targetMinion = towerAttackZone.targetMinion;
    }

    void Update()
    {
        GetComponent<ArrowMove>().target = targetMinion.transform;
    }

    private void OnBecameInvisible(){
        pool.Kill(this);
    }
    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject == targetMinion.gameObject)
        {
            targetMinion.TakeDamage(damage); 
            pool.Kill(this);
        }
    }
}




