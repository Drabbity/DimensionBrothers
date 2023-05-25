using UnityEngine;
using UnityEngine.SceneManagement;

namespace DimensionBrothers.Menu
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] private string _levelSceneNamePrefix;
        [SerializeField] private LevelSelector _levelSelectorPrefab;
        [SerializeField] private Transform _content;

        private void Start()
        {
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