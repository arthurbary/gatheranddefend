using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{

    public TMP_Text scoreText;
    void Start()
    {
        UpdatePlayerBoard();
    }
    public void UpdatePlayerBoard()
    {
        scoreText.SetText($"Score: {PlayerData.score}  Wood: {PlayerData.wood}  Stone {PlayerData.stone}");
    }
}
