using System;

public class GameMenu : BaseMenu
{
    private void OnEnable()
    {
        GameManager.OnGameEnded += HandleGameEnd;
    }

    private void HandleGameEnd()
    {
        UIManager.instance.ShowMenu(MenuType.EndGame);
    }

    public void OnMainMenuClicked()
    {
        UIManager.instance.ShowMenu(MenuType.Main);
    }

    private void OnDestroy()
    {
        GameManager.OnGameEnded -= HandleGameEnd;
    }
}
