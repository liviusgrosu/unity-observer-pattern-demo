using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    public Text scoreUI;

    public void Start()
    {
        Coin.CoinCollected += IncrementScore;
    }

    public void IncrementScore()
    {
        score++;
        scoreUI.text = $"Score: {score}";
    }

    public void OnDisable()
    {
        Coin.CoinCollected -= IncrementScore;
    }
}
