using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public static int scoreForTower;
    [SerializeField] public static int scoreForForge;
    [SerializeField] public static int scoreForGym;
    [SerializeField] public static int scoreForLab;
    [SerializeField] public static bool towerEnable = false;
    [SerializeField] public static bool forgeEnable = false;
    [SerializeField] public static bool gymEnable = false;
    [SerializeField] public static bool labEnable = false;

    private void OnEnable()
    {
        PlayerData.OnScoreReached += HandleScoreReached;
    }

    private void OnDisable()
    {
        PlayerData.OnScoreReached -= HandleScoreReached;
    }
    private void HandleScoreReached()
    {
        if(!towerEnable && PlayerData.score > scoreForTower)
        {
            towerEnable = true;
            // activation du button
        }
        else if (!forgeEnable && PlayerData.score > scoreForForge)
        {
            forgeEnable = true;
        }
        else if (!gymEnable && PlayerData.score > scoreForGym)
        {
            gymEnable = true;
        }
        else if (!labEnable && PlayerData.score > scoreForLab)
        {
            labEnable = true;
        }
    }

}
