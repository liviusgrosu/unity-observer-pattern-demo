using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    private int _score;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Text _scoreOverlayUI, _scoreGameOverUI;
    public static event Action SceneChanged;

    public void Start()
    {
        // Add as observer when coin is collected and when player dies
        Coin.CoinCollected += IncrementScore;
        Enemy.EnemyTouched += GameOverToggle;
    }

    public void IncrementScore()
    {
        // Increment the score and update the text
        _score++;
        _scoreOverlayUI.text = $"Score: {_score}";
    }

    private void GameOverToggle()
    {
        // Toggle the game over screen
        _gameOverScreen.SetActive(true);
        _scoreGameOverUI.text = $"Score: {_score}";
    }

    public void RestartButtonPress()
    {
        SceneChanged?.Invoke();
        // Restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnDisable()
    {
        Coin.CoinCollected -= IncrementScore;
        Enemy.EnemyTouched -= GameOverToggle;
    }
}
