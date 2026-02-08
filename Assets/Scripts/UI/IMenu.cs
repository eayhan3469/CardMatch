public interface IMenu
{
    MenuType Type { get; }
    void Show();
    void Hide();
}

public enum MenuType : byte
{
    Main,
    Game
}