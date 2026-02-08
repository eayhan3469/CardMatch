using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    private Queue<CardController> _selectionQueue = new Queue<CardController>();

    private void Start()
    {
        CardController.CardClicked += HandleCardClick;
    }

    private void HandleCardClick(CardController card)
    {
        card.Flip(true);
        _selectionQueue.Enqueue(card);

        if (_selectionQueue.Count >= 2)
        {
            CardController first = _selectionQueue.Dequeue();
            CardController second = _selectionQueue.Dequeue();
            StartCoroutine(ProcessMatchRoutine(first, second));
        }
    }

    private IEnumerator ProcessMatchRoutine(CardController first, CardController second)
    {
        yield return new WaitUntil(() => first.State == CardState.Front && second.State == CardState.Front);

        yield return new WaitForSeconds(0.3f);

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

    private void OnDestroy()
    {
        CardController.CardClicked -= HandleCardClick;
    }
}
