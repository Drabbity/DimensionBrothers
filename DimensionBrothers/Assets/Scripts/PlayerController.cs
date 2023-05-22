using UnityEngine;
using UnityEngine.InputSystem;

namespace DimensionBrothers.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _groundedDistance = 0.05f;
        [SerializeField] private LayerMask _jumpLayerMask;
        [SerializeField] private float _jumpHoldTime;
        [SerializeField] private float _gravity;
        [SerializeField] private float _maxGravityFallSpeed;
        [SerializeField] private float _coyoteJumpTimeGap;

        [SerializeField] private SpriteRenderer _renderer;
        [field: SerializeField] public bool IsActive { get; set; }

        private Rigidbody2D _rigidBody;
        private BoxCollider2D _collider;

        private PlayerInputActions _playerInput;
        private InputAction _move;
        private InputAction _jump;

        private float _moveInput = 0f;
        private bool _hasJumpInput = false;

        private bool _isGrounded = false;
        private Vector2 _velocity = default;

        private float _jumpTimer = 0;
        private float _coyoteJumpTimer = 0;
        private bool _isJumping = false;
        private bool _hasJumped = true;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _playerInput = new PlayerInputActions();
        }

        private void Start()
        {
            Physics2D.queriesHitTriggers = false;
        }

        private void OnEnable()
        {
            _move = _playerInput.Player.Move;
            _jump = _playerInput.Player.Jump;
            _move.Enable();
            _jump.Enable();
        }

        private void OnDisable()
        {
            _move.Disable();
            _jump.Disable();
        }

        private void Update()
        {
            ReadInput();
        }

        private void FixedUpdate()
        {
            ReadInfo();
            if (IsGrounded())
                _renderer.color = Color.blue;
            else
                _renderer.color = Color.red;

            CalculateMove();
            CalculateGravity();
            CalculateCoyoteJump();
            CalculateJump();

            ExecutePhysics();
        }

        private void ReadInput()
        {
            if (IsActive)
            {
                _moveInput = _move.ReadValue<Vector2>().x;
                _hasJumpInput = _jump.IsPressed();
            }
            else
            {
                _moveInput = 0f;
                _hasJumpInput = false;
            }
        }

        private void ReadInfo()
        {
            _isGrounded = IsGrounded();

            if (_isGrounded)
                _hasJumped = false;
        }

        private void CalculateMove()
        {
            _velocity = new Vector2(_moveInput * _moveSpeed, _rigidBody.velocity.y);
        }

        private void CalculateGravity()
        {
            if (!_isGrounded)
            {
                _velocity.y -= _gravity * Time.deltaTime;

                if (_velocity.y > _maxGravityFallSpeed)
                    _velocity.y = _maxGravityFallSpeed;
            }
            else
            {
                _velocity.y = 0;
            }
        }

        private void CalculateJump()
        {
            if ((_isGrounded || _coyoteJumpTimer < _coyoteJumpTimeGap) && _hasJumpInput && !_hasJumped)
            {
                _hasJumped = true;
                _isJumping = true;
                _jumpTimer = 0f;
                _velocity.y = _jumpForce;
            }
            else if (_hasJumpInput && _jumpTimer < _jumpHoldTime && _isJumping)
            {
                _jumpTimer += Time.fixedDeltaTime;
                _velocity.y = _jumpForce;
            }
            else if (!_hasJumpInput)
            {
                _isJumping = false;
            }
        }

        private void ExecutePhysics()
        {
            _rigidBody.velocity = _velocity;
        }

        private void CalculateCoyoteJump()
        {
            if (!IsGrounded())
            {
                _coyoteJumpTimer += Time.fixedDeltaTime;
            }
            else
            {
                _coyoteJumpTimer = 0;
            }
        }

        private bool IsGrounded()
        {
            return GetGround(_groundedDistance).collider;
        }

        private RaycastHit2D GetGround(float distance)
        {
            return Physics2D.BoxCast(_collider.bounds.center, new Vector2(_collider.bounds.size.x / 1.2f, _collider.bounds.size.y), 0, Vector2.down, distance, _jumpLayerMask);
        }
    }
}