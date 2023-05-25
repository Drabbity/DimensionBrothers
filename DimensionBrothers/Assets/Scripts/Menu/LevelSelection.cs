using UnityEngine;

namespace DimensionBrothers.Menu
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] private LevelSelector _levelSelectorPrefab;
        [SerializeField] private Transform _content;

        private string _levelSceneNamePrefix;

        private void Start()
        {
            _levelSceneNamePrefix = GameManager.Instance.LevelSceneNamePrefix;
            LoadLevelSelectors();
        }

        private void LoadLevelSelectors()
        {
            string sceneName = _levelSceneNamePrefix + "1";
            for (int i = 1; Application.CanStreamedLevelBeLoaded(sceneName); i++, sceneName = _levelSceneNamePrefix + i.ToString())
            {
                Instantiate(_levelSelectorPrefab, _content).Initialize(sceneName, i);
            }
        }
    }
}