using UnityEngine;

namespace DimensionBrothers.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _menus;

        private GameObject _currentMenu = null;

        private void Start()
        {
            SwitchMenu(0);
        }

        public void SwitchMenu(int _menuIndex)
        {
            if (!_currentMenu)
            {
                CloseAllMenus();
            }
            else
            {
                _currentMenu.SetActive(false);
            }

            _currentMenu = _menus[_menuIndex];
            _currentMenu.SetActive(true);
        }

        private void CloseAllMenus()
        {
            foreach (var menu in _menus)
            {
                menu.SetActive(false);
            }
        }
    }
}