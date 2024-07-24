// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;

// public class RegularMinionFactory : MinionFactory
// {
//     [SerializeField] float cooldown = 1.5f;
//     [SerializeField] GameObject prefab;
//     [SerializeField] private Transform launchPoint;


//     private IEnumerator Create()
//     {
//         bool isEnemy = transform.parent.GetComponent<Building>().isEnemy;
//         while (true)
//         {
//             if (pool != null)
//             {
//                 RegularMinionPoolMember poolMember = pool.Spawn(launchPoint.position, launchPoint.rotation, isEnemy);
//                 poolMember.Initialize();
//             }
//             else
//             {
//                 GameObject newMember = Instantiate(prefab, launchPoint.position, launchPoint.rotation);
//                 newMember.GetComponent<Minion>().isEnemy = isEnemy;
//                 newMember.GetComponent<Minion>().Initialize();
//             }
//             yield return new WaitForSeconds(cooldown);
//         }
//     }
// }
