using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    private List<CardController> _activeCards = new List<CardController>();

    private void Start()
    {
        CardController.CardClicked += HandleCardClick;
    }

    private void HandleCardClick(CardController card)
    {
        _activeCards.Add(card);
        card.Flip(true, () =>
        {
            if (_activeCards.Count >= 2) 
            {
                CheckMatch();
            }
        });
    }

    private void CheckMatch()
    {
        CardController first = _activeCards[0];
        CardController second = _activeCards[1];
        _activeCards.RemoveRange(0, 2);

        if (first.CardID == second.CardID)
        {
            first.MarkAsMatched();
            second.MarkAsMatched();
        }
        else
        {
            first.Flip(false);
            second.Flip(false);
        }
    }
}
