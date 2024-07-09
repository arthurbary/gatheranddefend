using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wood : Resource
{
    [SerializeField] private int subtractor = 3;
    void Start()
    {
        Type = ResourceType.WOOD;
        Subtractor = subtractor;
        GenerateLifeAndAmount();
    }
}
