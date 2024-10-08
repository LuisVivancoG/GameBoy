using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerControls;

    [SerializeField] private UIManager _uiManager;

    private void OnEnable()
    {
        _playerControls.actions["Start_Button"].started += Pause;
        _playerControls.actions["Select_Button"].started += Selection;
    }
    private void Pause(InputAction.CallbackContext context)
    {
        _uiManager.TogglePause();
        if (_uiManager._paused)
        {
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
        }
    }
    private void Selection(InputAction.CallbackContext context)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
