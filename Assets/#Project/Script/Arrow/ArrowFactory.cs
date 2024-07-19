using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : MonoBehaviour
{
    [SerializeField] float cooldown = 0.5f;
    [SerializeField] GameObject prefab;
    [SerializeField]private ArrowPool pool;
    [SerializeField] private Transform launchPoint;
    private TowerStateMachine towerStateMachine;
    void Start()
    {
        towerStateMachine = transform.parent.GetComponentInChildren<TowerStateMachine>();
        cooldown = towerStateMachine.damageRate;

        if (pool == null)
        {
            pool = GetComponent<ArrowPool>();
        }
        if (pool == null)
        {
            pool = FindObjectOfType<ArrowPool>();
        }
    }

    public IEnumerator Create()
    {
            if (pool != null)
            {
                ArrowPoolMember arrow = pool.Spawn(launchPoint.position,launchPoint.rotation);
                arrow.Initialize(towerStateMachine);
            }
            else
            {
                GameObject newArrow = Instantiate(prefab, launchPoint.position, launchPoint.rotation);
                newArrow.GetComponent<ArrowPoolMember>().Initialize(towerStateMachine);
            }
            yield return new WaitForSeconds(cooldown);
    }
}
