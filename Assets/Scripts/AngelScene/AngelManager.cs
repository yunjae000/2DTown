
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    private UIManager uiManager;
    public static bool isFiirstLoading = true;

    public static GameManager Instance
    {
        get { return gameManager; }
    }

    private int currentScore = 0;

  
    public UIManager UIManager { get { return uiManager; } }
    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        if (isFiirstLoading) 
        {
            StartGame();
        }
        else
        {
            isFiirstLoading = false;
        }
            uiManager.UpdateScore(0);
    }
    public void StartGame()
    {
        uiManager.SetPlayGame();
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        uiManager.SetGameOver();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score;

        Debug.Log("Score: " + currentScore);
        uiManager.UpdateScore(currentScore);
    }

}
