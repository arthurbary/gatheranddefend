using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected int Damage { get; set; }
    protected float Cooldown { get; set; }
    [SerializeField] WeaponMovement weaponMovement;
    [SerializeField] GameObject hitEffect;
    private bool isRunning = false;

    void OnTriggerEnter(Collider other)
    {
        if(weaponMovement.isCooldownActive) GiveDamage(other.gameObject);
    }

    protected IEnumerator _GiveDamage(GameObject other)
    {
        isRunning = true;
        
        if(other.GetComponent<Minion>() != null && other.GetComponent<Minion>().isEnemy)
        {
            other.GetComponent<Minion>().TakeDamage(Damage, other);
        } 
        else if (other.GetComponent<Building>() != null && other.GetComponent<Building>().isEnemy)
        {
            //other.GetComponent <Building>().TakeDamage(Damage);
            //Debug.Log($"Hitting building{other.GetComponent<Building>().name} at life {other.GetComponent<Building>().Life}");

        }
        GameObject.FindObjectOfType<DisplayManager>().UpdatePlayerBoard();
        yield return new WaitForSeconds(Cooldown);
        isRunning = false;
    }

    private void GiveDamage(GameObject other)
    {
        //if(isRunning) return;
        StartCoroutine(_GiveDamage(other));
    }
}
