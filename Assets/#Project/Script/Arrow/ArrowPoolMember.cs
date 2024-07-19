using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoolMember : MonoBehaviour
{
    public ArrowPool pool;
    private Minion targetMinion;
    private int damage;
    internal bool hasBeenInitialized = false;
    internal void Initialize(TowerStateMachine towerStateMachine)
    {
        damage = towerStateMachine.damage;
        targetMinion = towerStateMachine.minionToKill[0];
    }

    void Update()
    {
        GetComponent<ArrowMove>().target = targetMinion.transform;
    }

    private void OnBecameInvisible(){
        pool.Kill(this);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Are they the same objact: {other.gameObject == targetMinion.gameObject}");
        Debug.Log($"{other.gameObject.GetInstanceID()}");
        Debug.Log($"{targetMinion.gameObject.GetInstanceID()}");
        if(other.gameObject == targetMinion.gameObject)
        {
            targetMinion.TakeDamage(damage); 
            pool.Kill(this);
        }
    }
}




