using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    public enum ResourceType
    {
        STONE = 1,
        WOOD = 2
    }
    public int Life { get; set; }
    public int Amount { get; set; }
    public ResourceType Type { get; set; }

    public abstract void GiveResource();
    public void GenerateLifeAndAmount()
    {
        System.Random rand = new System.Random();
        int randomNumber = rand.Next(0, 100);
        Life = randomNumber;
        Amount = Life * (Type == ResourceType.STONE ? 2 : 3);
    }
}
