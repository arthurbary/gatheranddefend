using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public static int life = 5;
    [SerializeField] public static int score = 0;
    [SerializeField] public static int wood = 0;
    [SerializeField] public static int stone = 0;
    public delegate void ScoreReachedHandler();
    public static event ScoreReachedHandler OnScoreReached;
    public static event ScoreReachedHandler OnScoreReachedEnemy;
    
    void Start()
    {   
        wood = 300;
        stone = 300;
        
    }
    public static void IncreaseScore(int scoreReward)
    {
        score += scoreReward;
        OnScoreReached?.Invoke();
        OnScoreReachedEnemy?.Invoke();

    }

    public static void Reset()
    {
        score = 0;
        wood = 0;
        stone = 0;
    }
}
