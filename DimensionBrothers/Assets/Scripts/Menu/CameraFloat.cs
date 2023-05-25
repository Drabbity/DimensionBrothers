using UnityEngine;

namespace DimensionBrothers.Menu
{
    public class CameraFloat : MonoBehaviour
    {
        [SerializeField] private float _floatAmplitude = 1f;
        [SerializeField] private float _floatSpeed = 0.5f;
        [SerializeField] private float _floatDamping = 0.5f;

        private Vector2 _offset;
        private Vector2 _followPosition;
        private Vector2 _direction;

        private Vector2 _velocity = Vector2.zero;
        private float _cameraZPosition;

        private void Awake()
        {
            _direction = GetRandomDirection();
        }

        private void Start()
        {
            _offset = transform.position;
            _followPosition = _offset;
            _cameraZPosition = transform.position.z;
        }

        private void Update()
        {
            GetNextFollowPosition();
            Follow();
        }

        private void Follow()
        {
            Vector3 newPosition = Vector2.SmoothDamp(transform.position, _followPosition, ref _velocity, _floatDamping);
            newPosition.z = _cameraZPosition;
            transform.position = newPosition;
        }

        private void GetNextFollowPosition()
        {
            var relativePosition = _followPosition - _offset;
            _followPosition += _direction * _floatSpeed * Time.deltaTime;

            if ((relativePosition.x > _floatAmplitude && _direction.x > 0) ||
                (relativePosition.x < -_floatAmplitude && _direction.x < 0) ||
                (relativePosition.y > _floatAmplitude && _direction.y > 0) ||
                (relativePosition.y < -_floatAmplitude && _direction.y < 0))
            {
                _direction = GetRandomDirection();
            }
        }

        private Vector2 GetRandomDirection()
        {
            float xValue = Random.Range(-1f, 1f);
            float yValue = Mathf.Sqrt(1 - Mathf.Pow(xValue, 2));

            if (Random.Range(0, 2) == 0)
            {
                yValue *= -1;
            }

            return new Vector2(xValue, yValue);
        }
    }
}