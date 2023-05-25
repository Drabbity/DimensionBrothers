using UnityEngine;

namespace DimensionBrothers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _nextLevelButton;

        private void Start()
        {
            ValidateNextLevelButton();
            _victoryScreen.SetActive(false);
        }

        private void ValidateNextLevelButton()
        {
            if (!GameManager.Instance.DoesNextLevelExist())
                _nextLevelButton.SetActive(false);
        }

        public void OpenVictoryScreen()
        {
            _victoryScreen.SetActive(true);
        }
        public void LoadNextLevel()
        {
            GameManager.Instance.LoadNextLevel();
        }

        public void LoadMenu()
        {
            GameManager.Instance.LoadMenu();
        }
    }
}