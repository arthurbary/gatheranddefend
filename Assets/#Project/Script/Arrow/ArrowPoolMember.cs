using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoolMember : MonoBehaviour
{
    public ArrowPool pool;
    private Minion targetMinion;
    private int damage;
    internal bool hasBeenInitialized = false;
    private ArrowMove arrowMove;

    void Update()
    {
        if(targetMinion.canFly)
        {
            foreach (Transform child in targetMinion.gameObject.transform)
            {
                if (child.CompareTag(tag))
                {
                    GetComponent<ArrowMove>().target = child;
                    break;
                }
            }
        }
        else 
        {
            GetComponent<ArrowMove>().target = targetMinion.transform;
        }
    }
    internal void Initialize(TowerStateMachine towerStateMachine)
    {
        damage = towerStateMachine.damage;
        targetMinion = towerStateMachine.minionToKill[0];
    }

    private void OnBecameInvisible(){
        pool.Kill(this);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"What the arrow touch {other.gameObject.name}");
        if(other.gameObject == targetMinion.gameObject)
        {
            targetMinion.TakeDamage(damage); 
            pool.Kill(this);
        }
    }
}




