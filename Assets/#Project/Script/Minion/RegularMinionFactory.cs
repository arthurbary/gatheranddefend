using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RegularMinionFactory : MonoBehaviour
{
    [SerializeField] float cooldown = 1.5f;
    [SerializeField] GameObject prefab;
    [SerializeField]private RegularMinionPool pool;
    [SerializeField] private Transform launchPoint;
    void Start()
    {
        if (pool == null)
        {
            pool = GetComponent<RegularMinionPool>();
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
                RegularMinionPoolMember poolMember = pool.Spawn(launchPoint.position, launchPoint.rotation, isEnemy);
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
