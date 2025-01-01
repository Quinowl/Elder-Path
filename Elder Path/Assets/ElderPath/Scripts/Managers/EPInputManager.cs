using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EPInputManager : MonoBehaviour
{

    [SerializeField] private PlayerInput playerInput;

    private InputAction movementInputAction;
    private InputAction jumpInputAction;
    private InputAction attackInputAction;
    private InputAction dashInputAction;
    private InputAction resetInputAction;

    public float MoveInput { get; private set; }
    public bool JumpInputPressed { get; private set; }
    public bool JumpInputHeld { get; private set; }
    public bool JumpInputReleased { get; private set; }
    public bool AttackInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool ResetInput { get; private set; }
    public bool IsGamepad { get; private set; }

    private bool isInputEnabled = true;
    private string lastDevideUsed;

    public static EPInputManager Instance { get; private set; }

    public Action OnInputDeviceChanged;

    private void OnEnable()
    {
        InputSystem.onActionChange += ChangeInputAction;
    }

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
        if (!playerInput) GetComponent<PlayerInput>();
        InitializeInputActions();
    }

    private void Update()
    {
        if (isInputEnabled) ReadInputs();
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -= ChangeInputAction;
    }

    public void SetEnableInput(bool enable) => isInputEnabled = enable;

    private void ChangeInputAction(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            InputAction receivedInputAction = (InputAction)obj;
            InputDevice currentDevice = receivedInputAction.activeControl.device;
            if (lastDevideUsed == currentDevice.name) return;
            lastDevideUsed = currentDevice.name;
            IsGamepad = !(currentDevice.name.Equals("Keyboard") || currentDevice.name.Equals("Mouse"));
            OnInputDeviceChanged?.Invoke();
        }
    }

    private void InitializeInputActions()
    {
        isInputEnabled = true;
        movementInputAction = playerInput.actions[Constants.Inputs.PLAYER_MOVE_ACTION];
        jumpInputAction = playerInput.actions[Constants.Inputs.PLAYER_JUMP_ACTION];
        attackInputAction = playerInput.actions[Constants.Inputs.PLAYER_ATTACK_ACTION];
        dashInputAction = playerInput.actions[Constants.Inputs.PLAYER_DASH_ACTION];
        resetInputAction = playerInput.actions[Constants.Inputs.PLAYER_RESET_ACTION];
    }

    private void ReadInputs()
    {
        MoveInput = movementInputAction.ReadValue<float>();
        JumpInputPressed = jumpInputAction.WasPressedThisFrame();
        JumpInputHeld = jumpInputAction.IsPressed();
        JumpInputReleased = jumpInputAction.WasReleasedThisFrame();
        AttackInput = attackInputAction.WasPressedThisFrame();
        DashInput = dashInputAction.WasPressedThisFrame();
        ResetInput = resetInputAction.WasPressedThisFrame();
    }
}