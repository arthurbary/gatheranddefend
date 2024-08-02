using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public void OnEndGame(bool isEnemy)
    {
        Debug.Log("END OF GAME");
        PlayerData.Reset();
        ScoreManager.Reset();
        SceneManager.LoadScene(isEnemy ? "WinMenu" : "LostMenu" );
    }
}
