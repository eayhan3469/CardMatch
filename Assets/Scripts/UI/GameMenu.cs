public class GameMenu : BaseMenu
{
    public void OnMainMenuClicked()
    {
        UIManager.instance.ShowMenu(MenuType.Main);
    }
}
