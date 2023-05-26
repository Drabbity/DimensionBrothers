using UnityEngine;

namespace DimensionBrothers.Dimension
{
    public class Goal : MonoBehaviour
    {
        [SerializeField] private string _playerTag;
        [SerializeField] private int _playerCount = 2;

        private LevelManager _levelManager;

        private int _playersAtGoal = 0;

        private void Start()
        {
            _levelManager = LevelManager.Instance;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_playerTag))
            {
                _playersAtGoal++;

                if (_playersAtGoal >= _playerCount)
                {
                    _levelManager.OpenVictoryScreen();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(_playerTag))
            {
                _playersAtGoal--;
            }
        }
    }
}