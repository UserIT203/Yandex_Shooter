using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<MenuBaseUI> _menu;
    [SerializeField] private MenuBaseUI _defaultMenu;

    private MenuBaseUI _currentOpenMenu;
    private Dictionary<string, MenuBaseUI> _menuDictanonary = new Dictionary<string, MenuBaseUI>();

    private void Awake()
    {
        CloseAllMenu();

        foreach (MenuBaseUI group in _menu)
            _menuDictanonary.Add(group.GetType().Name, group);
    }

    private void Start()
    {
        OpenMenu(_defaultMenu.GetType().Name);
    }

    private void CloseAllMenu()
    {
        foreach(MenuBaseUI group in _menu)
        {
            group.GetComponent<CanvasGroup>().Deactivate();
        }
    }

    public void OpenMenu(string menuName)
    {
        _currentOpenMenu?.CloseMenu();
        _currentOpenMenu =  _menuDictanonary[menuName];
        _currentOpenMenu.OpenMenu();

        Debug.Log("Open menu " + menuName);
    }
}
