using System;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    public int Rows;
    public int Columns;
    public int Score;
    public int TurnCount;
    public int Combo;

    public List<CardSaveState> CardStates = new List<CardSaveState>();
}

[Serializable]
public class CardSaveState
{
    public int CardID;
    public bool IsMatched;

    public int BoardIndex;
}