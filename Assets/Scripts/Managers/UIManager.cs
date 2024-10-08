using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _levelHUD;
    public bool _paused { get; private set; }

    [SerializeField] private Image _boostBar;
    public float _reduceSpeed = 0.1f;
    [SerializeField] private float _restoreAmount = 0.33f;
    [SerializeField] private float _lerpSpeed = 2f;

    private bool _isButtonHeld = false;
    public float _targetBoostAmount;
    [SerializeField] private PlayerInput _playerControls;

    private void OnEnable()
    {
    }
    private void Start()
    {
        _targetBoostAmount = .15f;
        _boostBar.fillAmount = _targetBoostAmount;
    }

    private void Update()
    {
        if (_isButtonHeld && _targetBoostAmount > 0)
        {
            _targetBoostAmount -= _reduceSpeed * Time.deltaTime;
            _targetBoostAmount = Mathf.Clamp(_targetBoostAmount, 0f, 1f);
        }
        _boostBar.fillAmount = Mathf.Lerp(_boostBar.fillAmount, _targetBoostAmount, Time.deltaTime * _lerpSpeed);
    }

    public void TogglePause()
    {
        if(_pauseMenu != null)
        {
            if (!_paused)
            {
                _pauseMenu.SetActive(true);
                _levelHUD.SetActive(false);
                _paused = true;
            }
            else
            {
                _pauseMenu.SetActive(false);
                _levelHUD.SetActive(true);
                _paused = false;
            }
        }
        else
        {
            return;
        }
    }

    public void OnButtonPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isButtonHeld = true;
        }
        else if (context.canceled)
        {
            _isButtonHeld = false;
        }
    }
    public void RestoreBoost()
    {
        _targetBoostAmount += _restoreAmount;
        _targetBoostAmount = Mathf.Clamp(_targetBoostAmount, 0f, 1f);
    }
}
