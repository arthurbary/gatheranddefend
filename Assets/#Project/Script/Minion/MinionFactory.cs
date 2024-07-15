using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionFactory : MonoBehaviour
{
    protected MinionPool Pool;
    protected float Cooldown = 1.0f;
    protected GameObject Prefab;
    protected Transform LaunchPoint;
    public bool CanLaunchMinion = false;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if(CanLaunchMinion)
        {
            if (Pool == null)
            {
                Pool = GetComponent<MinionPool>();
            }
            StartCoroutine(Create());
        }
    }

    private IEnumerator Create()
    {
        bool isEnemy = transform.parent.GetComponent<Building>().isEnemy;

        while (true)
        {
            if (Pool != null)
            {
                MinionPoolMember PoolMember = Pool.Spawn(LaunchPoint.position, LaunchPoint.rotation, isEnemy);
                PoolMember.Initialize();
            }
            else
            {
                GameObject newMember = Instantiate(Prefab, LaunchPoint.position, LaunchPoint.rotation);
                newMember.GetComponent<Minion>().isEnemy = isEnemy;
                newMember.GetComponent<Minion>().Initialize();
            }
            yield return new WaitForSeconds(Cooldown);
        }
    }
}
