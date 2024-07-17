using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackZone : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageRate = 1.0f;
    private bool isTargetReachable = false;
    private bool isAttacking = false;
    public Minion targetMinion;
    private Building tower;
    void Awake()
    {
        tower = transform.parent.GetComponent<Building>();
    }
    void OnTriggerEnter(Collider other)
    {
        Minion otherMinion = other.GetComponent<Minion>();
        if( !isAttacking && otherMinion != null && otherMinion.isEnemy != tower.isEnemy )
        {
            isTargetReachable = true;
            targetMinion = otherMinion;
            StartCoroutine(Attack(targetMinion));
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(targetMinion != null && other.gameObject == targetMinion.gameObject && other.gameObject.activeSelf) isTargetReachable = false;
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
