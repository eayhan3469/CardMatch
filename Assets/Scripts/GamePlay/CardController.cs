using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image displayImage;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite frontSprite;

    public static Action<CardController> CardClicked;

    public int CardID { get; private set; }
    public CardState State { get; private set; } = CardState.Back;

    public void Initialize(int id, Sprite frontSprite)
    {
        CardID = id;
        this.frontSprite = frontSprite;
        displayImage.sprite = backSprite;
    }

    private void HandleSelection()
    {
        CardClicked?.Invoke(this);
    }

    public void Flip(bool toFront, Action onComplete = null)
    {
        StartCoroutine(FlipRoutine(toFront, onComplete));
    }

    private IEnumerator FlipRoutine(bool toFront, Action onComplete)
    {
        State = CardState.Flipping;

        float duration = 0.2f;
        float elapsed = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion midRotation = Quaternion.Euler(0f, 90f, 0f);
        Quaternion endRotation = toFront ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, midRotation, elapsed / (duration / 2));
            yield return null;
        }

        displayImage.sprite = toFront ? frontSprite : backSprite;

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(midRotation, endRotation, elapsed / (duration / 2));
            yield return null;
        }

        State = toFront ? CardState.Front : CardState.Back;
        onComplete?.Invoke();
    }

    public void MarkAsMatched()
    {
        State = CardState.Matched;
        displayImage.raycastTarget = false;
        // TODO: Write matched process.
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (State == CardState.Flipping || State == CardState.Matched || State == CardState.Front)
            return;

        HandleSelection();
    }
}

public enum CardState : byte
{
    Back,
    Flipping,
    Front,
    Matched
}