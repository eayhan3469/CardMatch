using System;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI matchesText;
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private GameObject comboContainer;

    private int _score = 0;
    private int _turn = 0;
    private int _combo = 0;

    public int Score => _score;
    public int Turn => _turn;
    public int Combo => _combo;

    private void OnEnable()
    {
        GameManager.OnMatched += AddScore;
        GameManager.OnMismatch += HandleMismatch;

        comboContainer.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.OnMatched -= AddScore;
        GameManager.OnMismatch -= HandleMismatch;

        comboContainer.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        matchesText.text = _score.ToString();
        turnsText.text = _turn.ToString();

        comboText.text = string.Format("{0}x", _combo);
        comboContainer.gameObject.SetActive(_combo > 0);
    }

    private void HandleMismatch()
    {
        _combo = 0;

        AddTurn();
    }

    private void AddTurn()
    {
        _turn++;

        UpdateUI();
    }

    private void AddScore()
    {
        if (_combo > 0)
            _score += _combo;
        else
            _score++;

        _combo++;

        AddTurn();
        UpdateUI();
    }

    public void SetScore(int score)
    {
        _score = score;
        UpdateUI();
    }

    public void SetTurn(int turn)
    {
        _turn = turn;
        UpdateUI();
    }

    public void SetCombo(int combo)
    {
        _combo = combo;
        UpdateUI();
    }
}
