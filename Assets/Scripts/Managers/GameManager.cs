using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int maxRowCount;
    [SerializeField] private int minRowCount;
    [SerializeField] private int maxColumnCount;
    [SerializeField] private int minColumnCount;

    public static Action OnGameStarted;
    public static Action OnGameContinued;
    public static Action OnGameEnded;

    public static Action OnFlip;
    public static Action OnMatched;
    public static Action OnMismatch;

    private ScoreCounter _scoreCounter;
    private int _currentLevel;

    public ScoreCounter ScoreCounter => _scoreCounter;

    private void OnEnable()
    {
        OnGameStarted += HandleGameStart;
        OnGameContinued += HandleGameContinue;
    }

    private void OnDisable()
    {
        OnGameStarted -= HandleGameStart;
        OnGameContinued -= HandleGameContinue;
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
        var rowCount = UnityEngine.Random.Range(minRowCount, maxRowCount);
        var columnCount = UnityEngine.Random.Range(minColumnCount, maxColumnCount);

        BoardManager.instance.InitializeBoard(rowCount, columnCount);

        ScoreCounter.SetScore(0);
        ScoreCounter.SetTurn(0);

        CreateSavePoint();
    }

    private void HandleGameContinue()
    {
        GameSaveData savedData = SaveManager.Load();

        if (savedData != null)
        {
            BoardManager.instance.LoadGame(savedData);

            ScoreCounter.SetScore(savedData.Score);
            ScoreCounter.SetTurn(savedData.TurnCount);
        }
    }

    public void CheckGameFinish()
    {
        int totalPairsNeeded = (BoardManager.instance.CurrentRows * BoardManager.instance.CurrentCols) / 2;

        if (ScoreCounter.Score >= totalPairsNeeded)
        {
            OnGameEnded?.Invoke();
            SaveManager.DeleteSave();
        }
    }

    public void CreateSavePoint()
    {
        GameSaveData data = new GameSaveData
        {
            Score = ScoreCounter.Score,
            TurnCount = ScoreCounter.Turn,
            Rows = BoardManager.instance.CurrentRows,
            Columns = BoardManager.instance.CurrentCols
        };

        foreach (var card in BoardManager.instance.SpawnedCards)
        {
            data.CardStates.Add(new CardSaveState
            {
                CardID = card.CardID,
                IsMatched = card.State == CardState.Matched
            });
        }

        SaveManager.Save(data);
    }
}
