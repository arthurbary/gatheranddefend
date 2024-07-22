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
    DisplayManager displayManager;
    void Start()
    {
        displayManager = GameObject.FindObjectOfType<DisplayManager>();
    }
    public static void IncreaseScore(int scoreReward)
    {
        score += scoreReward;
        OnScoreReached?.Invoke();
        OnScoreReachedEnemy?.Invoke();

    }
}
