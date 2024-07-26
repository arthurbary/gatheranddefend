using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Forge", menuName = "GandD/Forge", order = 1)]
public class ForgeData : ScriptableObject
{
    public int scoreToGetForge;
    public int forgeWoodCost = 1;
    public int forgeStoneCost = 1;
    public int forgeLife = 1;
    public int forgeScoreReward = 1; 
    public float forgeMinionRate = 1.0f;

}
