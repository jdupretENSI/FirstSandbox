using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputController : MonoBehaviour
{
    public static event Action<bool> OnInputDeviceChanged;

    private CharacterHandler _characterHandler;
    private PlayerInput _playerInput;
    private bool _isControllerConnected;

    private void Awake()
    {
        _characterHandler = GetComponent<CharacterHandler>();
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput == null) throw new NullReferenceException("PlayerInputManager is null");
    }

    private void Start()
    {
        // Cursor.visible = false;
    }

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;

        // Bind input actions
        _playerInput.actions["Move"].performed += OnMove;
        _playerInput.actions["Move"].canceled += OnMove;
        
        DetectCurrentInputDevice();
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;

        // Unbind input actions
        _playerInput.actions["Move"].performed -= OnMove;
        _playerInput.actions["Move"].canceled -= OnMove;
    }
    
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
        {
            DetectCurrentInputDevice();
        }
    }

    private void DetectCurrentInputDevice()
    {
        _isControllerConnected = Gamepad.all.Count > 0;
        OnInputDeviceChanged?.Invoke(_isControllerConnected);

        Debug.Log(_isControllerConnected
            ? "Controller connected: Switching to Gamepad controls."
            : "No controller connected: Switching to Keyboard/Mouse controls.");
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _characterHandler.Move = context.ReadValue<Vector2>();
    }
}