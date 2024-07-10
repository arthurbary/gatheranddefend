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
    private bool isEnemy = false;
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
        while (true)
        {
            if (pool != null)
            {
                RegularMinionPoolMember poolMember = pool.Spawn(launchPoint.position, launchPoint.rotation);
                Debug.Log($"Spawn is the parent {transform.parent.name}, an enemy: {transform.parent.GetComponent<Building>().isEnemy}");

                poolMember.isEnemy = transform.parent.GetComponent<Building>().isEnemy;
            }
            else
            {
                GameObject newMember = Instantiate(prefab, launchPoint.position, launchPoint.rotation);
                Debug.Log($"Initiate is the parent {transform.parent.name}, an enemy: {transform.parent.GetComponent<Building>().isEnemy}");
                newMember.GetComponent<Minion>().isEnemy = transform.parent.GetComponent<Building>().isEnemy;
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
}
