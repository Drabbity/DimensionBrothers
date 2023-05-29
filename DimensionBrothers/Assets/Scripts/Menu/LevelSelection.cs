using UnityEngine;

namespace DimensionBrothers.Menu
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] private LevelSelector _levelSelectorPrefab;
        [SerializeField] private Transform _content;

        private string _levelSceneNamePrefix;
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _levelSceneNamePrefix = _gameManager.LevelSceneNamePrefix;
            LoadLevelSelectors();
        }

        private void LoadLevelSelectors()
        {
            string sceneName = _levelSceneNamePrefix + "1";
            for (int i = 1; Application.CanStreamedLevelBeLoaded(sceneName); i++, sceneName = _levelSceneNamePrefix + i.ToString())
            {
                Instantiate(_levelSelectorPrefab, _content).Initialize(sceneName, i, i <= _gameManager.UnlockedLevels);
            }
        }
    }
}