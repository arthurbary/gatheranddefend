using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : MonoBehaviour
{
    [SerializeField] float cooldown = 0.5f;
    [SerializeField] GameObject prefab;
    [SerializeField]private ArrowPool pool;
    [SerializeField] private Transform launchPoint;
    private TowerAttackZone towerAttackZone;
    void Start()
    {
        towerAttackZone = transform.parent.GetComponentInChildren<TowerAttackZone>();
        cooldown = towerAttackZone.damageRate;

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
                arrow.Initialize(towerAttackZone);
            }
            else
            {
                GameObject newArrow = Instantiate(prefab, launchPoint.position, launchPoint.rotation);
                newArrow.GetComponent<ArrowPoolMember>().Initialize(towerAttackZone);
            }
            yield return new WaitForSeconds(cooldown);
    }
}
