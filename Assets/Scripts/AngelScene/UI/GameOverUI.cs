using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private string targetSceneName = "SamPleScene";
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshPro GameOverText;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        restartButton.onClick.AddListener(OnClickRestartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickExitButton()
    {
        
        SceneManager.LoadScene(targetSceneName);
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}