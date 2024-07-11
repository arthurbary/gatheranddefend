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
        Transform otherParent = other.transform.parent;
        Debug.Log($"Enemy name: {otherParent.gameObject.name}");
        Debug.Log($"Tower is not attacking: {!isAttacking}");
        Debug.Log($"Minion is available: {otherParent.GetComponent<Minion>() != null}");
        Debug.Log($"Minion is enemy: {!otherParent.GetComponent<Minion>().isEnemy != isEnemy}");
        if( !isAttacking && otherParent.GetComponent<Minion>() != null)
        {
            isTargetReachable = true;
            targetMinion = otherParent.GetComponent<Minion>();
            StartCoroutine(Attack(targetMinion));
        }
    }
    void OnTriggerExit(Collider other) 
    { 
        if(targetMinion != null && other.transform.parent == targetMinion.gameObject) isTargetReachable = false;
    }

    public IEnumerator Attack(Minion targetMinion)
    {
        isAttacking = true;
        while (isTargetReachable && isAttacking)
        {
                if (targetMinion != null)
            {
                targetMinion.TakeDamage(damage);
            }
            else
            {
                Debug.Log("TARGET IS OUT OF REACH OR DESTROYED");
                isAttacking = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
