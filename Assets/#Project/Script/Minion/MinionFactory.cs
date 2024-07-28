using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionFactory : MonoBehaviour
{
    [SerializeField]protected MinionPool pool;
    [SerializeField]public float Cooldown = 1.0f;
    [SerializeField]protected GameObject Prefab;
    [SerializeField]protected Transform LaunchPoint;
    public bool CanLaunchMinion = false;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if(CanLaunchMinion)
        {
            if (pool == null)
            {
                pool = GetComponent<MinionPool>();
            }
            StartCoroutine(Create());
        }
    }

    private IEnumerator Create()
    {
        bool isEnemy = transform.parent.GetComponent<Building>().isEnemy;

        while (true)
        {
            if (pool != null)
            {
                MinionPoolMember PoolMember = pool.Spawn(LaunchPoint.position, LaunchPoint.rotation, isEnemy);
                PoolMember.state = MinionState.Initialize;
            }
            else
            {
                GameObject newMember = Instantiate(Prefab, LaunchPoint.position, LaunchPoint.rotation);
                newMember.GetComponent<Minion>().isEnemy = isEnemy;
                newMember.GetComponent<Minion>().state = MinionState.Initialize;
            }
            yield return new WaitForSeconds(Cooldown);
        }
    }
}
