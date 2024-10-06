using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerControls;
    private Vector2 _movementInput;

    [Header("Welcome prompt")]
    [SerializeField] private Text _prompt;
    [SerializeField] float _flickrSeconds;
    [SerializeField] float _transition;
    private bool _inputRecieved;

    private void OnEnable()
    {
        _playerControls.actions["Move"].performed += OnMovePerformed;
        _playerControls.actions["Move"].canceled += OnMoveCanceled;

        _playerControls.actions["A_Button"].started += OnAPressed;
        _playerControls.actions["Start_Button"].started += OnStartPressed;
    }

    private void OnDisable()
    {
        _playerControls.actions["Move"].performed -= OnMovePerformed;
        _playerControls.actions["Move"].canceled -= OnMoveCanceled;

        _playerControls.actions["A_Button"].started -= OnAPressed;
        _playerControls.actions["Start_Button"].started -= OnStartPressed;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForInput());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _movementInput = Vector2.zero;
    }
    private void OnAPressed(InputAction.CallbackContext context)
    {
        if (!_inputRecieved)
        {
            _inputRecieved = true;
        }

        Debug.Log("A Pressed");
    }
    private void OnStartPressed(InputAction.CallbackContext context)
    {
        if (!_inputRecieved)
        {
            _inputRecieved = true;
        }

        Debug.Log("Start Pressed");
    }

    IEnumerator WaitForInput()
    {
        float minSize = 13;
        float maxSize = 16;
        float timer = 0;


        while (_inputRecieved == false)
        {
            float newSize = Mathf.Lerp(minSize, maxSize, Mathf.PingPong(timer, 1f));
            _prompt.fontSize = (int)newSize;
            timer += Time.deltaTime / _transition;

            yield return null;
        }
    }
}
