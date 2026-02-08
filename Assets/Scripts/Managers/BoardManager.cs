using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoSingleton<BoardManager>
{
    [Header("References")]
    [SerializeField] private List<CardData> cardPool;
    [SerializeField] private RectTransform gridContainer;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private CardController cardPrefab;

    [Header("Settings")]
    [SerializeField] private float paddingPercentage = 0.05f;
    [SerializeField] private float cardAspectRatio = 0.75f;

    private LevelData _currentLevelData;
    private List<CardController> _spawnedCards = new List<CardController>();

    public void InitializeBoard(LevelData levelData)
    {
        _currentLevelData = levelData;

        ClearBoard();
        SetupGrid();
        SpawnCards();
    }

    private void SetupGrid()
    {
        float containerWidth = gridContainer.rect.width;
        float containerHeight = gridContainer.rect.height;

        float totalSpacingX = gridLayoutGroup.spacing.x * (_currentLevelData.Columns - 1);
        float totalSpacingY = gridLayoutGroup.spacing.y * (_currentLevelData.Rows - 1);

        float availableWidth = containerWidth - totalSpacingX;
        float availableHeight = containerHeight - totalSpacingY;

        float maxCellW = availableWidth / _currentLevelData.Columns;
        float maxCellH = availableHeight / _currentLevelData.Rows;

        float finalW, finalH;

        float heightBasedOnWidth = maxCellW / cardAspectRatio;

        if (heightBasedOnWidth <= maxCellH)
        {
            finalW = maxCellW;
            finalH = heightBasedOnWidth;
        }
        else
        {
            finalH = maxCellH;
            finalW = maxCellH * cardAspectRatio;
        }

        gridLayoutGroup.cellSize = new Vector2(finalW, finalH);
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = _currentLevelData.Columns;
    }

    private void SpawnCards()
    {
        int totalCards = _currentLevelData.Rows * _currentLevelData.Columns;

        if (totalCards % 2 == 1)
            totalCards--;

        int pairsNeeded = totalCards / 2;
        List<CardData> selectedData = new List<CardData>();

        for (int i = 0; i < pairsNeeded; i++)
        {
            CardData data = cardPool[i % cardPool.Count];
            selectedData.Add(data);
            selectedData.Add(data);
        }

        ShuffleList(selectedData);

        for (int i = 0; i < totalCards; i++)
        {
            CardController card = Instantiate(cardPrefab, gridContainer);
            card.Initialize(selectedData[i].ID, selectedData[i].FrontSprite);
            _spawnedCards.Add(card);
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void ClearBoard()
    {
        foreach (var card in _spawnedCards)
        {
            if (card != null)
                Destroy(card.gameObject);
        }

        _spawnedCards.Clear();
    }
}
