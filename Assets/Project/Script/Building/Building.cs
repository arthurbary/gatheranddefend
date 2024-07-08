using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    protected int woodCost;
    protected int stoneCost;
    protected int level;
    protected int life;
    public abstract void GenerateMinion();
}
