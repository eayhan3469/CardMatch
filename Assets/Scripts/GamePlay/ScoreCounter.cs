using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI matchesText;
    [SerializeField] private TextMeshProUGUI turnsText;

    private int _score = 0;
    private int _turn = 0;

    public int Score => _score;
    public int Turn => _turn;

    private void OnEnable()
    {
        GameManager.OnMatched += AddScore;

        GameManager.OnMatched += AddTurn;
        GameManager.OnMismatch += AddTurn;
    }

    private void OnDisable()
    {
        GameManager.OnMatched -= AddScore;

        GameManager.OnMatched -= AddTurn;
        GameManager.OnMismatch -= AddTurn;
    }

    private void UpdateUI()
    {
        matchesText.text = _score.ToString();
        turnsText.text = _turn.ToString();
    }

    private void AddTurn()
    {
        _turn++;
        UpdateUI();
    }

    private void AddScore()
    {
        _score++;
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
}
