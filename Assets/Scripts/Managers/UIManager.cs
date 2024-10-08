using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _levelHUD;
    public bool _paused { get; private set; }

    [SerializeField] private Image _boostBar;
    private float _currentBoostAmount;
    private float _newBoostAmount;

    private void Start()
    {
        _currentBoostAmount = 1f;
        //_newBoostAmount = 1f;
    }

    private void Update()
    {
        //UpdateChargesHUD();
        _boostBar.fillAmount = Mathf.Lerp(_currentBoostAmount, _newBoostAmount, (Time.deltaTime));
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

    public void RemoveCharge()
    {
        _newBoostAmount = _newBoostAmount - .10f;
        //_newBoostAmount = Mathf.Min(_currentBoostAmount, 0f);
    }
    public void AddCharge()
    {
        _newBoostAmount = _newBoostAmount + .10f;
        //_newBoostAmount = Mathf.Max(_currentBoostAmount, 1f);
    }
    private void UpdateChargesHUD()
    {
        if (_currentBoostAmount != _newBoostAmount)
        {
            _currentBoostAmount = _newBoostAmount;
            _boostBar.fillAmount = Mathf.Lerp(_currentBoostAmount, _newBoostAmount, (Time.deltaTime));
        }
    }
}
