using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DimensionBrothers.Menu
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] TextMeshProUGUI _text;

        public void Initialize(string sceneName, int levelNumber)
        {
            _button.onClick.AddListener(() => { GameManager.Instance.LoadScene(sceneName); });
            _text.text = levelNumber.ToString();
        }
    }
}