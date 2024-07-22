using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int scoreForTower;
    [SerializeField] private int scoreForForge;
    [SerializeField] private int scoreForGym;
    [SerializeField] private int scoreForLab;
    [SerializeField] private GameObject towerButton;
    [SerializeField] private GameObject forgeButton;
    [SerializeField] private GameObject gymButton;
    [SerializeField] private GameObject labButton;
    public static bool towerEnable = false;
    public static bool forgeEnable = false;
    public static bool gymEnable = false;
    public static bool labEnable = false;
    

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
        if(!towerEnable && PlayerData.score >= scoreForTower)
        {
            towerEnable = true;
            towerButton.SetActive(true);
        }
        else if (!forgeEnable && PlayerData.score > scoreForForge)
        {
            forgeEnable = true;
            forgeButton.SetActive(true);
        }
        else if (!gymEnable && PlayerData.score > scoreForGym)
        {
            gymEnable = true;
            gymButton.SetActive(true);
        }
        else if (!labEnable && PlayerData.score > scoreForLab)
        {
            labEnable = true;
            labButton.SetActive(true);
        }
    }

}
