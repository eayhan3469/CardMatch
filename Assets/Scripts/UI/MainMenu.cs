using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    [SerializeField] private Button continueButton;

    private void OnEnable()
    {
        continueButton.interactable = SaveManager.HasSave();
    }

    public void OnPlayClicked()
    {
        UIManager.instance.ShowMenu(MenuType.Game);
        SaveManager.DeleteSave();
        GameManager.OnGameStarted?.Invoke();
    }

    public void OnContinueClicked()
    {
        UIManager.instance.ShowMenu(MenuType.Game);
        GameManager.OnGameContinued?.Invoke();
    }
}
