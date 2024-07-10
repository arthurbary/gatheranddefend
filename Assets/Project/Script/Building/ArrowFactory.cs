using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
        [SerializeField] float cooldown = 0.5f;
    [SerializeField] GameObject prefab;
    [SerializeField]private ArrowPool pool;
    [SerializeField] private Transform launchPoint;
    [SerializeField] public bool canShoot = true;
    void Start()
    {

        if (pool == null)
        {
            pool = GetComponent<ArrowPool>();
        }
        if (pool == null)
        {
            pool = FindObjectOfType<ArrowPool>();
        }
    }

    void Update()
    {
        if(!canShoot) canShoot = true;
    }

    private IEnumerator Create()
    {
            if (pool != null)
            {
                pool.Spawn(launchPoint.position,launchPoint.rotation);
            }
            else
            {
                Instantiate(prefab, launchPoint.position, launchPoint.rotation);
            }
            yield return new WaitForSeconds(cooldown);
    }

    public void Shoot()
    {
        if(canShoot)
        {

        }
    }
}
