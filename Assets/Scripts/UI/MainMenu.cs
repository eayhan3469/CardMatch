public class MainMenu : BaseMenu
{
    public void OnPlayClicked()
    {
        UIManager.instance.ShowMenu(MenuType.Game);
    }
}
