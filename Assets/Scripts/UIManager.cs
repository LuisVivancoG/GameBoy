using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    public bool _paused { get; private set; }

    private int _maxCharges;
    private int _currentcharges;
    [SerializeField] private Image[] _chargesBoost;

    private void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        Transform childTransform = canvas.transform.Find("PauseUI");
        _pauseMenu = childTransform.gameObject;

    }
    public void TogglePause()
    {
        if(_pauseMenu != null)
        {
            if (!_paused)
            {
                _pauseMenu.SetActive(true);
                _paused = true;
            }
            else
            {
                _pauseMenu.SetActive(false);
                _paused = false;
            }
        }
        else
        {
            return;
        }
    }

    public void UpdateChargesHUD()
    {
        for (int i = 0; i < _chargesBoost.Length; i++)
        {
            _chargesBoost[i].enabled = (i < _currentcharges);
        }
    }
}
