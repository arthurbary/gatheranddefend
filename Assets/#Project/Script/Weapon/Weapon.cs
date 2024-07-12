using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected int Damage { get; set; }
    protected float DamageRate { get; set; }
    [SerializeField] WeaponMovement weaponMovement;
    private bool isRunning = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Hit {other.name} and cooldown is {weaponMovement.isCooldownActive}");
        if(weaponMovement.isCooldownActive) GiveDamage(other.gameObject);
    }

    protected IEnumerator _GiveDamage(GameObject other)
    {
        isRunning = true;
        if(other.GetComponent<Minion>() != null)
        {
            Debug.Log("Hitting Minion");
            other.GetComponent<Minion>().TakeDamage(Damage);
        } 
        else if (other.GetComponent<Building>() != null)
        {
            Debug.Log("Hitting building");
            other.GetComponent <Building>().TakeDamage(Damage);
        }
        yield return new WaitForSeconds(0.1f);
        isRunning = false;
    }

    private void GiveDamage(GameObject other)
    {
        if(isRunning) return;
        StartCoroutine(_GiveDamage(other));
    }
}
