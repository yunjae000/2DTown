using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum UIState
{
    Home,
    Game,
      GameOver,
}

public class UIManager : MonoBehaviour
{
    HomeUI homeUI;
   
    GameOverUI gameOverUI;
    private UIState currentState;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);
      
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);

        ChangeState(UIState.Home);
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

 

   

    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI.SetActive(currentState);
       gameOverUI.SetActive(currentState);
    }
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}