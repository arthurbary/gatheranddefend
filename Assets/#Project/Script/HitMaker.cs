using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMaker : MonoBehaviour
{
    public GameObject hitPrefab;

    public void CreateHit(GameObject other)
    {
        //Ajouter l'arrow
        if(other.GetComponent<Minion>() != null && !other.GetComponent<Minion>().isEnemy)
        {
            return;
        }
        else if(other.GetComponent<Building>() != null && !other.GetComponent<Building>().isEnemy)
        {
            return;
        }
        
        else
        {
            //Debug.Log($"hit point {hitPoint.name}, preFab: {hitPoint.hitPrefab.name}, transform: {hitPoint.transform}");
            GameObject spawnedHit = Instantiate(hitPrefab, transform.position, Quaternion.identity);
            spawnedHit.transform.LookAt(Camera.main.transform);
        }
        
    }
}
