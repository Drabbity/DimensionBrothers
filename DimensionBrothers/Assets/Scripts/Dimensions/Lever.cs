using UnityEngine;
using UnityEngine.Events;

namespace DimensionBrothers.Dimension
{
    public class Lever : MonoBehaviour
    {
        [field: SerializeField] public UnityEvent OnSwitchOn { get; set; }
        [field: SerializeField] public UnityEvent OnSwitchOff { get; set; }

        [SerializeField] private SpriteRenderer _leverSpriteRenderer;
        [SerializeField] private string _playerTagName;

        private float _previousPlayerXPosition = 0;
        private bool _isPlayerInTrigger = false;
        private bool _isLeverOn = false;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.CompareTag(_playerTagName))
            {
                var currentPlayerXPosition = collision.transform.position.x;

                if (!_isPlayerInTrigger)
                {
                    _isPlayerInTrigger = true;
                    _previousPlayerXPosition = currentPlayerXPosition;
                }
                else
                {
                    if (_previousPlayerXPosition < currentPlayerXPosition && !_isLeverOn)
                    {
                        _leverSpriteRenderer.flipX = true;
                        _isLeverOn = true;
                        OnSwitchOn.Invoke();
                    }
                    else if(_previousPlayerXPosition > currentPlayerXPosition && _isLeverOn)
                    {
                        _leverSpriteRenderer.flipX = false;
                        _isLeverOn = false;
                        OnSwitchOff.Invoke();
                    }
                }

                _previousPlayerXPosition = currentPlayerXPosition;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.CompareTag(_playerTagName))
            {
                _isPlayerInTrigger = false;
            }
        }
    }
}