using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;

    public void Start()
    {
        Coin.CoinCollected += IncrementScore;
    }

    public void IncrementScore()
    {
        score++;
    }
}
