using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public static int life = 5;
    [SerializeField] public static int score = 0;
    [SerializeField] public static int wood = 0;
    [SerializeField] public static int stone = 0;

    void Awake()
    {
        wood = 10;
        stone = 10;
    }
}
