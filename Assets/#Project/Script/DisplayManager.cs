using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text woodText;
    public TMP_Text stoneText;
    public TMP_Text woodTowerText;
    public TMP_Text stoneTowerText;
    public TMP_Text woodForgeText;
    public TMP_Text stoneForgeText;
    public TMP_Text woodGymText;
    public TMP_Text stoneGymText;
    public TMP_Text woodLabText;
    public TMP_Text stoneLabText;
    private PlayerManager playerManager;
    void Start()
    {
        
        UpdatePlayerBoard();
        ButtonSetUp();
    }
    public void UpdatePlayerBoard()
    {
        scoreText.SetText($"Score: {PlayerData.score}");
        woodText.SetText($" : {PlayerData.wood}");
        stoneText.SetText($" : {PlayerData.stone}");
    }
    void ButtonSetUp()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        woodTowerText.SetText($"{playerManager.towerWoodCost}");
        stoneTowerText.SetText($"{playerManager.towerStoneCost}");
        woodForgeText.SetText($"{playerManager.forgeWoodCost}");
        stoneForgeText.SetText($"{playerManager.forgeStoneCost}");
        woodGymText.SetText($"{playerManager.gymWoodCost}");
        stoneGymText.SetText($"{playerManager.gymStoneCost}");
        woodLabText.SetText($"{playerManager.labWoodCost}");
        stoneLabText.SetText($"{playerManager.labStoneCost}");
    }
    
}
