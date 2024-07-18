using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackZone : MonoBehaviour
{
    [SerializeField] public int damage = 1;
    [SerializeField] public float damageRate = 1.0f;
    private bool isTargetReachable = false;
    private bool isAttacking = false;
    public Minion targetMinion;
    private Building tower;

    public ArrowFactory arrowFactory;
    void Awake()
    {
        tower = transform.parent.GetComponent<Building>();
        arrowFactory = transform.parent.GetComponentInChildren<ArrowFactory>();
    }

    /* 
    travailler en Queue
    la target est le premier minion de la queue
    il rentre dans la queue quand il rentre dans la zone
    il sort de la queue s'il est mort(inactive)
    il sort de la queue s'il sort de la zone
    
    */
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
                /*
                c'est la balle qui doit donner les dammage
                targetMinion.TakeDamage(damage); 
                */
                StartCoroutine(arrowFactory.Create());
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
