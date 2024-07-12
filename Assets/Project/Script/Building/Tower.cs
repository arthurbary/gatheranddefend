using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Tower : Building
{
    [SerializeField] private int woodCost = 1;
    [SerializeField] private int stoneCost = 1;
    [SerializeField] private int life = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageRate = 1.0f;
    private bool isTargetReachable = false;
    private bool isAttacking = false;
    public Minion targetMinion;

    void Start()
    {
        WoodCost = woodCost;
        StoneCost = stoneCost;
        Life = life;
        Type = BuildingType.TOWER;
    }

    void OnTriggerEnter(Collider other)
    {
        Minion otherMinion = other.GetComponent<Minion>();
        if( !isAttacking && otherMinion != null && otherMinion.isEnemy != isEnemy )
        {
            isTargetReachable = true;
            targetMinion = otherMinion;
            StartCoroutine(Attack(targetMinion));
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.activeSelf && other.gameObject == targetMinion.gameObject) isTargetReachable = false;
    }

    public IEnumerator Attack(Minion targetMinion)
    {
        isAttacking = true;
        while (isTargetReachable && isAttacking)
        {
                if (targetMinion.gameObject.activeSelf)
            {
                targetMinion.TakeDamage(damage);
            }
            else
            {
                Debug.Log("TARGET IS OUT OF REACH OR DESTROYED");
                isAttacking = false;
            }
            yield return new WaitForSeconds(damageRate);
        }
    }
}
