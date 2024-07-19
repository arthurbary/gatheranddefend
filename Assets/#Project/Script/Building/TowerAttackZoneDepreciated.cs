using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackZoneDepreciated : MonoBehaviour
{
    [SerializeField] public int damage = 1;
    [SerializeField] public float damageRate = 1.0f;
    private bool isTargetReachable = false;
    private bool isAttacking = false;
    public Minion targetMinion;
    private Building tower;
    private TowerStateMachine towerStateMachine;
    Queue<Minion> minionToKill;

    public ArrowFactory arrowFactory;
    void Awake()
    {
        tower = transform.parent.GetComponent<Building>();
        arrowFactory = transform.parent.GetComponentInChildren<ArrowFactory>();
        towerStateMachine =  transform.parent.GetComponent<TowerStateMachine>();
    }

    void Update()
    {
        while(towerStateMachine.state == TowerState.Firing)
        {
            StartCoroutine(Attack(minionToKill.Peek()));
        }
    }

    void OnTriggerEnter(Collider other)
    {

        Minion otherMinion = other.GetComponent<Minion>();

        if( otherMinion != null && !minionToKill.Contains(otherMinion) && otherMinion.isEnemy != tower.isEnemy )
        {
            minionToKill.Enqueue(otherMinion);
           /*
           isTargetReachable = true;
            targetMinion = otherMinion;
            StartCoroutine(Attack(targetMinion)); 
            */
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
