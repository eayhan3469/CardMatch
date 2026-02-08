using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
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
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.interactable = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;
        gameObject.SetActive(false);
    }
}
