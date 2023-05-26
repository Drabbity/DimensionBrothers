using UnityEngine;

namespace DimensionBrothers
{
    public class LevelManager : MonoBehaviour
    {
        #region Singleton
        private static LevelManager _instance;
        public static LevelManager Instance { get { return _instance; } }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Debug.LogErrorFormat("[Singleton] Trying to instantiate a second instance of singleton class {0} from {1}", GetType().Name, gameObject.name);
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
        #endregion
        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private GameObject _nextLevelButton;
        [SerializeField] private GameObject _pauseButton;

        public bool IsGamePaused { get; private set; } = false;

        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.Instance;

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