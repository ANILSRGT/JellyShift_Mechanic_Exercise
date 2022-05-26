using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject successPanel;

    private void Awake()
    {
        HideAllPanel();
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// This button is used to start the game.
    /// </summary>
    public void StartGameButton()
    {
        GameManager.Instance.StartGame();
        HideAllPanel();
        gamePanel.SetActive(true);
    }

    /// <summary>
    /// This button is used to restart the game.
    /// </summary>
    public void RestartLevelButton()
    {
        GameManager.Instance.RestartLevel();
        HideAllPanel();
        gamePanel.SetActive(true);
    }

    /// <summary>
    /// This button is used to go to the next level.
    /// </summary>
    public void NextLevelButton()
    {
        GameManager.Instance.NextLevel();
        HideAllPanel();
        gamePanel.SetActive(true);
    }

    /// <summary>
    /// Called when the player fails.
    /// </summary>
    public void OnFail()
    {
        HideAllPanel();
        failPanel.SetActive(true);
    }

    /// <summary>
    /// Called when the player succeeds.
    /// </summary>
    public void OnSuccess()
    {
        HideAllPanel();
        successPanel.SetActive(true);
    }

    /// <summary>
    /// Hides all panels.
    /// </summary>
    public void HideAllPanel()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        failPanel.SetActive(false);
        successPanel.SetActive(false);
    }
}