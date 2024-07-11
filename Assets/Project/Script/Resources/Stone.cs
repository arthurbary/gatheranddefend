using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Resource
{
    // Start is called before the first frame update
    [SerializeField] private int substractor = 2;
    [SerializeField] private int life;
    void Start()
    {
        Type = ResourceType.STONE;
        Subtractor = substractor;
        WeaponMovement = GameObject.FindWithTag("WeaponBasic").GetComponent<WeaponMovement>();
        GenerateLifeAndAmount();
        Life = (life != 0) ? life : 1;
    }
}


