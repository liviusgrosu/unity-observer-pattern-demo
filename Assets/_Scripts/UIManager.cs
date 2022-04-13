using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    private int score;
    public GameObject GameOverScreen;
    public Text scoreOverlayUI, scoreGameOverUI;

    public void Start()
    {
        Coin.CoinCollected += IncrementScore;
        Enemy.EnemyTouched += GameOverToggle;
    }

    public void IncrementScore()
    {
        // Increment the score and update the text
        score++;
        scoreOverlayUI.text = $"Score: {score}";
    }

    private void GameOverToggle()
    {
        // Toggle the game over screen
        GameOverScreen.SetActive(true);
        scoreGameOverUI.text = $"Score: {score}";
    }

    public void RestartButtonPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnDisable()
    {
        Coin.CoinCollected -= IncrementScore;
    }
}
