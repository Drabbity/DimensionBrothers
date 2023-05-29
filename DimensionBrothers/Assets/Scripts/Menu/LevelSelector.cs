using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DimensionBrothers.Menu
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] Image _renderer;
        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] Color _interactableColor;
        [SerializeField] Color _notInteractableColor;

        public void Initialize(string sceneName, int levelNumber, bool isInteractable)
        {
            _button.onClick.AddListener(() => { GameManager.Instance.LoadScene(sceneName); });
            _button.interactable = isInteractable;

            if (isInteractable)
                _renderer.color = _interactableColor;
            else
                _renderer.color = _notInteractableColor;

            _text.text = levelNumber.ToString();
        }
    }
}