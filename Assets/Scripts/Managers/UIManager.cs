using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _levelHUD;
    public bool _paused { get; private set; }

    private int _currentcharges;
    private int _maxCharges = 3;
    [SerializeField] private Image[] _chargesBoost;

    private void Awake()
    {
        //GameObject canvas = GameObject.Find("Canvas");
        //Transform childTransform = canvas.transform.Find("PauseUI");
        //_pauseMenu = childTransform.gameObject;
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
        if (_currentcharges > 0)
        {
            _currentcharges--;
            UpdateChargesHUD();
        }
    }
    public void AddCharge()
    {
        if (_currentcharges < _maxCharges)
        {
            _currentcharges++;
            UpdateChargesHUD();
        }
    }

    private void UpdateChargesHUD()
    {
        //Debug.Log("Event Called");
        for (int i = 0; i < _chargesBoost.Length; i++)
        {
            _chargesBoost[i].enabled = (i < _currentcharges);
        }
    }
}
