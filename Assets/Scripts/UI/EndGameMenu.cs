using System;
using TMPro;
using UnityEngine;

public class EndGameMenu : BaseMenu
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _turnText;

    private void OnEnable()
    {
        GameManager.OnGameEnded += HandleEndGame;
    }

    private void HandleEndGame()
    {
        var score = GameManager.instance.ScoreCounter.Score.ToString();
        var turn = GameManager.instance.ScoreCounter.Turn.ToString();

        _scoreText.text = string.Format("Score:{0}", score);
        _turnText.text = string.Format("Turn:{0}", turn);
    }

    public void OnNewGameClicked()
    {
        UIManager.instance.ShowMenu(MenuType.Game);
        SaveManager.DeleteSave();
        GameManager.OnGameStarted?.Invoke();
    }

    private void OnDestroy()
    {
        GameManager.OnGameEnded -= HandleEndGame;
    }
}
