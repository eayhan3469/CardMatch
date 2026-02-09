using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int maxRowCount;
    [SerializeField] private int minRowCount;
    [SerializeField] private int maxColumnCount;
    [SerializeField] private int minColumnCount;

    public static Action OnGameStarted;
    public static Action OnGameEnded;

    public static Action OnFlip;
    public static Action OnMatched;
    public static Action OnMismatch;

    private ScoreCounter _scoreCounter;
    private int _currentLevel;

    private void OnEnable()
    {
        OnGameStarted += HandleGameStart;
    }

    private void OnDisable()
    {
        OnGameStarted -= HandleGameStart;
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            _currentLevel = 0;
            PlayerPrefs.SetInt("Level", _currentLevel);
        }
        else
        {
            _currentLevel = PlayerPrefs.GetInt("Level");
        }

        _scoreCounter = FindAnyObjectByType<ScoreCounter>();
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
