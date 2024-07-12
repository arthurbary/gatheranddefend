using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected int Damage { get; set; }
    protected float DamageRate { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"name object hit {other.name}");
        GiveDamage(other.gameObject);
    }

    protected void GiveDamage(GameObject other)
    {
        if(other.GetComponent<Minion>() != null)
        {
            other.GetComponent<Minion>().TakeDamage(Damage);
        } 
        else if (other.GetComponent<Building>() != null)
        {
            other.GetComponent <Building>().TakeDamage(Damage);
        }
    }


}
