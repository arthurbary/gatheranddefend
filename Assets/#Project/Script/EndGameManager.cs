using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEndGame(bool isEnemy)
    {
        Debug.Log("END OF GAME");
        PlayerData.stone = 0;
        PlayerData.wood = 0;
        PlayerData.score = 0;        
        SceneManager.LoadScene(isEnemy ? "WinMenu" : "LostMenu" );
    }
}
