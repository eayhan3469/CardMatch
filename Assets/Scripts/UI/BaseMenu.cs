using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BaseMenu : MonoBehaviour, IMenu
{
    [SerializeField] private MenuType _type;

    public MenuType Type => _type;

    protected CanvasGroup CanvasGroup;

    protected virtual void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        if (!EnsureCanvasGroup())
            return;

        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.interactable = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (!EnsureCanvasGroup())
            return;

        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;
        gameObject.SetActive(false);
    }

    private bool EnsureCanvasGroup()
    {
        if (CanvasGroup != null)
            return true;

        CanvasGroup = GetComponent<CanvasGroup>();

        if (CanvasGroup == null)
        {
            Debug.LogError($"CanvasGroup component not found on {gameObject.name}. " +
                         "Please ensure it's attached or RequireComponent is working.", this);

            return false;
        }

        return true;
    }
}
