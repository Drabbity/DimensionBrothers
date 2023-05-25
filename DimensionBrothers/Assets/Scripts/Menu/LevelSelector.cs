using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DimensionBrothers.Menu
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] TextMeshProUGUI _text;

        public void Initialize(string sceneName, int levelNumber)
        {
            _button.onClick.AddListener(() => { SceneManager.LoadScene(sceneName); });
            _text.text = levelNumber.ToString();
        }
    }
}