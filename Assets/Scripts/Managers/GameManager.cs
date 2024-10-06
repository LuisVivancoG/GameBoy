using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerControls;

    [SerializeField] private UIManager _uiManager;

    //private static GameManager _instance;

    private void OnEnable()
    {
        _playerControls.actions["Start_Button"].started += Pause;
        _playerControls.actions["Select_Button"].started += Selection;
    }

    private void OnDisable()
    {
        _playerControls.actions["Start_Button"].started -= Pause;
        _playerControls.actions["Select_Button"].started -= Selection;
    }

    private void Awake()
    {
        //if(_instance == null)
        //{
        //    _instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    /*
     * -Game starts
     * -Call Scene manager
     * -Start main menu
     * -Call UI managers
     * -MainMenu wait for press button to play
     * -Press play or start / press quit
     * -Call scene manager
     * -Start lvl1
     * -Reset score to 0
     * -Reset timer to 0
     * -Countdown 3 seconds
     * -Initiate movement
     * 
     * 
     */

    private void Pause(InputAction.CallbackContext context)
    {
        _uiManager.TogglePause();
        if (_uiManager._paused)
        {
            Debug.Log("Game paused");
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
        Debug.Log("Select pressed");
    }
}
