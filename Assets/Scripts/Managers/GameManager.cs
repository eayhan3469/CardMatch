using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int maxRowCount;
    [SerializeField] private int minRowCount;
    [SerializeField] private int maxColumnCount;
    [SerializeField] private int minColumnCount;

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
        {
            BoardManager.instance.LoadGame(savedData);
        }
        else
        {
            var rowCount = UnityEngine.Random.Range(minRowCount, maxRowCount);
            var columnCount = UnityEngine.Random.Range(minColumnCount, maxColumnCount);

            BoardManager.instance.InitializeBoard(rowCount, columnCount);
        }
    }
}
