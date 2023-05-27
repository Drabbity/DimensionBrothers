using DG.Tweening;
using DimensionBrothers.Other;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DimensionBrothers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameObject _loader;
        [SerializeField] private Vector3 _transitionLoaderScale;
        [SerializeField] private float _transitionDuration;

        [SerializeField] private string MenuSceneName;
        [field: SerializeField] public string LevelSceneNamePrefix { get; private set; }

        private bool _isLoading = false;

        private void Start()
        {
            _loader.SetActive(false);
        }

        public void LoadScene(string levelName)
        {
            TransitionIn(() => 
            {
                SceneManager.LoadScene(levelName);
                TransitionOut();
            });
        }

        public void LoadNextLevel()
        {
            var currentLevel = GetCurrentLevel();
            var nextLevelName = LevelSceneNamePrefix + (currentLevel + 1).ToString();

            if (currentLevel == 0 || !Application.CanStreamedLevelBeLoaded(nextLevelName))
                LoadScene(MenuSceneName);
            else
            {
                LoadScene(nextLevelName);
            }
        }

        public bool DoesNextLevelExist()
        {
            var currentLevel = GetCurrentLevel();
            var nextLevelName = LevelSceneNamePrefix + (currentLevel + 1).ToString();

            return currentLevel != 0 && Application.CanStreamedLevelBeLoaded(nextLevelName);
        }

        public int GetCurrentLevel()
        {
            var levelName = SceneManager.GetActiveScene().name;

            if (IsSceneALevel(levelName))
            {
                var levelNumber = Int32.Parse(levelName.Substring(LevelSceneNamePrefix.Length, levelName.Length - LevelSceneNamePrefix.Length));
                return levelNumber;
            }
            else
                return 0;
        }

        public void LoadMenu()
        {
            LoadScene(MenuSceneName);
        }

        private bool IsSceneALevel(string levelName)
        {
            if (levelName.Contains(LevelSceneNamePrefix))
                return true;
            else
                return false;
        }

        private void TransitionIn(Action onComplete = null)
        {
            if(!_isLoading)
            {
                _isLoading = true;
                _loader.transform.localScale = Vector3.zero;
                _loader.SetActive(true);

                _loader.transform.DOScale(_transitionLoaderScale, _transitionDuration).SetEase(Ease.OutElastic, 3f).OnComplete(() => onComplete?.Invoke());
            }
        }
        private void TransitionOut(Action onComplete = null)
        {
            _loader.transform.DOScale(Vector3.zero, _transitionDuration).SetEase(Ease.OutElastic, 3f).OnComplete(() =>
            {
                onComplete?.Invoke();
                _loader.SetActive(false);
                _isLoading = false;
            });
        }
    }
}