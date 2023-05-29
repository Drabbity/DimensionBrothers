using DimensionBrothers.Audio;
using DimensionBrothers.Other;
using UnityEngine;

namespace DimensionBrothers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private GameObject _nextLevelButton;
        [SerializeField] private GameObject _pauseButton;

        public bool IsGamePaused { get; private set; } = false;

        private GameManager _gameManager;
        private AudioManager _audioManager;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _audioManager = AudioManager.Instance;

            ValidateNextLevelButton();
            _victoryScreen.SetActive(false);
            _pauseScreen.SetActive(false);
            _pauseButton.SetActive(true);
        }

        private void ValidateNextLevelButton()
        {
            if (!_gameManager.DoesNextLevelExist())
                _nextLevelButton.SetActive(false);
        }

        public void OpenVictoryScreen()
        {
            _pauseScreen.SetActive(false);
            _pauseButton.SetActive(false);
            _victoryScreen.SetActive(true);
            IsGamePaused = true;
            _audioManager.PlaySound("VICTORY");
            _gameManager.UnlockLevel(_gameManager.GetCurrentLevel() + 1);
        }

        public void TogglePauseScreen()
        {
            IsGamePaused = !IsGamePaused;

            _pauseButton.SetActive(!IsGamePaused);
            _pauseScreen.SetActive(IsGamePaused);
        }

        public void LoadNextLevel()
        {
            _gameManager.LoadNextLevel();
        }

        public void LoadMenu()
        {
            _gameManager.LoadMenu();
        }
    }
}