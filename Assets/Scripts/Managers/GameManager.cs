using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<LevelData> levels;

    public static Action OnGameStarted;

    private int _currentLevel;

    private void Awake()
    {
        OnGameStarted += HandleGameStart;

        if (!PlayerPrefs.HasKey("Level"))
        {
            _currentLevel = 0;
            PlayerPrefs.SetInt("Level", _currentLevel);
        }
        else
        {
            _currentLevel = PlayerPrefs.GetInt("Level");
        }
    }

    private void HandleGameStart()
    {
        GameSaveData savedData = SaveManager.Load();

        if (savedData != null)
            BoardManager.instance.LoadGame(savedData);
        else
            BoardManager.instance.InitializeBoard(levels[_currentLevel]);
    }
}
