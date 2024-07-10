using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMinionFactory : MonoBehaviour
{
    [SerializeField] float cooldown = 0.5f;
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
        if (pool == null)
        {
            pool = FindObjectOfType<RegularMinionPool>();
        }
    }

    private IEnumerator Create()
    {
            if (pool != null)
            {
                pool.Spawn(launchPoint.position,launchPoint.rotation);
            }
            else
            {
                GameObject newMember = Instantiate(prefab, launchPoint.position, launchPoint.rotation);
                //defnir s'il est un enemy via sont parent
                //PROVISOIR definir sa target
                    //prendre tout les batiments
                    //si le batiment n'est PAS du meme "coté que soit" le selectionner comme target
            }
            yield return new WaitForSeconds(cooldown);
    }
}
