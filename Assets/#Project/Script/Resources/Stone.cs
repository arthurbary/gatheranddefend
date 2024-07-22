using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Resource
{
    // Start is called before the first frame update
    [SerializeField] private int substractor = 2;
    void Awake()
    {
        Type = ResourceType.STONE;
        Subtractor = substractor;
        WeaponMovement = GameObject.FindWithTag("WeaponBasic").GetComponent<WeaponMovement>();
    }
}


