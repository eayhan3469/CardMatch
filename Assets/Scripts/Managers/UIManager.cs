using System.Collections.Generic;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<MenuType, IMenu> _menuDictionary = new Dictionary<MenuType, IMenu>();
    private IMenu _activeMenu;

    private void Awake()
    {
        InitializeMenus();
    }

    private void InitializeMenus()
    {
        var menus = GetComponentsInChildren<IMenu>(true);

        foreach (var menu in menus)
        {
            if (!_menuDictionary.ContainsKey(menu.Type))
            {
                _menuDictionary.Add(menu.Type, menu);
                menu.Hide();
            }
        }

        ShowMenu(MenuType.Main);
    }

    public void ShowMenu(MenuType type, bool hideActive = true)
    {
        if (!_menuDictionary.ContainsKey(type)) return;

        if (hideActive && _activeMenu != null)
        {
            _activeMenu.Hide();
        }

        _activeMenu = _menuDictionary[type];
        _activeMenu.Show();
    }
}
