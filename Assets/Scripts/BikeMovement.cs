using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BikeMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _frontWheel;
    [SerializeField] private Rigidbody2D _backWheel;
    [SerializeField] private Rigidbody2D _bodyBike;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotation;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpQueueTime = 0.2f;

    [SerializeField] private float _boostSpeed;
    private int _maxBoostCharges = 3;
    private int _currentCharges;

    [SerializeField] private PlayerInput _playerControls;
    private Vector2 _movementInput;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    private bool _isGrounded;
    private bool _jumpQueued = false;
    private float _jumpQueueTimer = 0f;

    public UnityEvent onPlayerBoosted;
    public UnityEvent onBoostCollected;
    public UnityEvent onScoring;

    [SerializeField] AudioManager _audioManager;

    [SerializeField] private string _nextLevelName;

    [SerializeField] private GameObject _exhaust;
    [SerializeField] private UIManager _uiManager;


    private void OnEnable()
    {
        _playerControls.actions["Move"].performed += OnMovePerformed;
        _playerControls.actions["Move"].canceled += OnMoveCanceled;

        _playerControls.actions["A_Button"].performed += OnUsingBoost;
        _playerControls.actions["A_Button"].canceled += OnCancelingBoost;
        _playerControls.actions["B_Button"].started += OnJump;
    }

    private void OnDisable()
    {
        _playerControls.actions["Move"].performed -= OnMovePerformed;
        _playerControls.actions["Move"].canceled -= OnMoveCanceled;

        _playerControls.actions["A_Button"].performed -= OnUsingBoost;
        _playerControls.actions["A_Button"].canceled -= OnCancelingBoost;
        _playerControls.actions["B_Button"].started -= OnJump;
    }



    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics2D.OverlapBox(_groundCheck.position, new Vector2(0.42f, 0.07f), 0, _groundMask);

        if (_movementInput.x != 0)
        {
            _frontWheel.AddTorque(-_movementInput.x * _speed * Time.fixedDeltaTime);
            _backWheel.AddTorque(-_movementInput.x * _speed * Time.fixedDeltaTime);

        }
        if (_movementInput.y != 0)
        {
            _bodyBike.AddTorque(_movementInput.y * _rotation * Time.fixedDeltaTime);
        }

        if (_isGrounded)
        {
            if (_jumpQueued && _jumpQueueTimer > 0f)
            {
                PerformJump();
            }
        }

        if (_jumpQueueTimer > 0f)
        {
            _jumpQueueTimer -= Time.deltaTime;
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _movementInput = Vector2.zero;
    }
    private void OnUsingBoost(InputAction.CallbackContext context)
    {
        if (_uiManager._targetBoostAmount > 0 && context.performed)
        {
            BoostFunction();
        }
    }
    private void OnCancelingBoost(InputAction.CallbackContext context)
    {
        _exhaust.SetActive(false);
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            PerformJump();
        }
        else
        {
            QueueJump();
        }
    }
    private void QueueJump()
    {
        _jumpQueued = true;
        _jumpQueueTimer = _jumpQueueTime;
        //Debug.Log("Jump queued");
    }

    private void PerformJump()
    {
        _bodyBike.velocity = new Vector2(_bodyBike.velocity.x, _jumpForce);
        //Debug.Log("Jump performed");

        _jumpQueued = false;
        _jumpQueueTimer = 0f;
    }
    void BoostFunction()
    {
        if (_uiManager._targetBoostAmount > 0)
        {
            onPlayerBoosted?.Invoke();
            _exhaust.SetActive(true);
            _bodyBike.velocity = transform.right * _boostSpeed;

            _uiManager._targetBoostAmount -= _uiManager._reduceSpeed * Time.deltaTime;
            _uiManager._targetBoostAmount = Mathf.Clamp(_uiManager._targetBoostAmount, 0f, 1f);
        }
        else
        {
            _exhaust.SetActive(false);
        }
    }
    void AddCharge(Collider2D other)
    {
        other.gameObject.SetActive(false);
        _currentCharges++;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boost")
        {
            if( _currentCharges < _maxBoostCharges )
            {
                AddCharge(other);
                onBoostCollected?.Invoke();
                _audioManager.Play("Boost");
            }
        }

        if (other.gameObject.tag == "Collectables")
        {
            other.gameObject.SetActive(false);
            onScoring?.Invoke();
            _audioManager.Play("Coin");
        }

        if (other.gameObject.tag == "Goal")
        {
            StartCoroutine(VictorySequence());
            _audioManager.Play("Goal");
        }
    }

    IEnumerator VictorySequence()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(_nextLevelName);
    }
}
