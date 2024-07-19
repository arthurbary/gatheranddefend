using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wood : Resource
{
    [SerializeField] private int subtractor = 3;
    [SerializeField] private int life;
    [SerializeField] private int amount;
    void Awake()
    {
        Type = ResourceType.WOOD;
        Subtractor = subtractor;
        WeaponMovement = GameObject.FindWithTag("WeaponBasic").GetComponent<WeaponMovement>();
        //GenerateLifeAndAmount();
        if(life !=0) Life = life;
    }
    void Update()
    {
        Debug.Log($"Wood amount {Amount}");
    }
}
