using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerMinionFactory : MonoBehaviour
{
    [SerializeField] float cooldown = 1.0f;
    [SerializeField] GameObject prefab;
    [SerializeField]private FlyerMinionPool pool;
    [SerializeField] private Transform launchPoint;
    void Start()
    {
        if (pool == null)
        {
            pool = GetComponent<FlyerMinionPool>();
        }
        StartCoroutine(Create());
    }
    void Update()
    {
    }

    private IEnumerator Create()
    {
        bool isEnemy = transform.parent.GetComponent<Building>().isEnemy;
        while (true)
        {
            if (pool != null)
            {
                FlyerMinionPoolMember poolMember = pool.Spawn(launchPoint.position, launchPoint.rotation, isEnemy);
                poolMember.Initialize();
            }
            else
            {
                GameObject newMember = Instantiate(prefab, launchPoint.position, launchPoint.rotation);
                newMember.GetComponent<Minion>().isEnemy = isEnemy;
                newMember.GetComponent<Minion>().Initialize();
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
}
