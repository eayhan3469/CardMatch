using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image displayImage;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite frontSprite;

    [SerializeField] private float flipDuration = 0.2f;

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
        GameManager.OnFlip?.Invoke();
    }

    private IEnumerator FlipRoutine(bool toFront, Action onComplete)
    {
        State = CardState.Flipping;
        float halfDuration = flipDuration / 2f;
        float elapsed = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion midRotation = Quaternion.Euler(0f, 90f, 0f);
        Quaternion endRotation = toFront ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);

        // First Half: 0 -> 90 degree
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, midRotation, elapsed / halfDuration);
            yield return null;
        }

        displayImage.sprite = toFront ? frontSprite : backSprite;
        elapsed = 0f;

        // Second Half: 90 -> 180/0 degree
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(midRotation, endRotation, elapsed / halfDuration);
            yield return null;
        }

        transform.rotation = endRotation; 
        State = toFront ? CardState.Front : CardState.Back;
        onComplete?.Invoke();
    }

    public void MarkAsMatched()
    {
        State = CardState.Matched;
        displayImage.raycastTarget = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (State == CardState.Flipping || State == CardState.Matched || State == CardState.Front)
            return;

        HandleSelection();
    }

    internal void InstantShowFront()
    {
        displayImage.sprite = frontSprite;
    }
}

public enum CardState : byte
{
    Back,
    Flipping,
    Front,
    Matched
}