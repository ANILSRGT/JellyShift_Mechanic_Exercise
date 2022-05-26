using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameStatus gameStatus;

    [Header("Level Info")]
    [SerializeField] private GameObject[] levelPrefabs;
    public LevelManager currentLevel;
    [SerializeField] private int currentLevelIndex;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        gameStatus = GameStatus.MENU;

        if (!PlayerPrefs.HasKey("LastLevelIndex"))
        {
            PlayerPrefs.SetInt("LastLevelIndex", 0);
        }
        currentLevelIndex = PlayerPrefs.GetInt("LastLevelIndex");

        LoadLevel();
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        gameStatus = GameStatus.PLAY;
        Events.OnGameStart?.Invoke();
    }

    /// <summary>
    /// Create the required level from the levelPrefabs array.
    /// </summary>
    public void LoadLevel()
    {
        if (currentLevel)
        {
            Destroy(currentLevel);
        }

        if (currentLevelIndex >= levelPrefabs.Length)
        {
            currentLevelIndex = 0;
            PlayerPrefs.SetInt("LastLevelIndex", 0);
        }

        currentLevel = Instantiate(levelPrefabs[currentLevelIndex]).GetComponent<LevelManager>();
        currentLevel.name = levelPrefabs[currentLevelIndex].name;
    }

    /// <summary>
    /// Loads the current level.
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Loads the next level.
    /// </summary>
    public void NextLevel()
    {
        PlayerPrefs.SetInt("LastLevelIndex", ++currentLevelIndex);
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Fail the level.
    /// </summary>
    public void Fail()
    {
        gameStatus = GameStatus.FAIL;
        Events.OnGameEnd?.Invoke();
        UIManager.Instance.OnFail();
    }

    /// <summary>
    /// Successful completion of the level.
    /// </summary>
    public void Success()
    {
        gameStatus = GameStatus.SUCCESS;
        Events.OnGameEnd?.Invoke();
        UIManager.Instance.OnSuccess();
    }

    public enum GameStatus
    {
        MENU,
        PLAY,
        FAIL,
        SUCCESS
    }
}