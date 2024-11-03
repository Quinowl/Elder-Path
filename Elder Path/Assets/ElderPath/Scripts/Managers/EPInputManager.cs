using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.InputSystem;

public class EPInputManager : MonoBehaviour {

    [SerializeField] private PlayerInput playerInput;

    private InputAction movementInputAction;
    private InputAction jumpInputAction;
    private InputAction attackInputAction;
    private InputAction dashInputAction;

    public float MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool IsGamepad { get; private set; }

    public static EPInputManager Instance {get; private set;}
    
    private void OnEnable() {
        InputSystem.onActionChange += ChangeInputAction;
    }

    private void Awake() {
        if (!Instance) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        if (!playerInput) GetComponent<PlayerInput>();
        InitializeInputActions();
    }

    private void Update() {
        ReadInputs();
    }

    private void OnDisable() {
        InputSystem.onActionChange -= ChangeInputAction;
    }

    private void ChangeInputAction(object obj, InputActionChange change) {
        if (change == InputActionChange.ActionPerformed) {
            InputAction receivedInputAction = (InputAction)obj;
            InputDevice lastDevice = receivedInputAction.activeControl.device;
            IsGamepad = !(lastDevice.name.Equals("Keyboard") || lastDevice.name.Equals("Mouse"));
        }
    }

    private void InitializeInputActions() {
        movementInputAction = playerInput.actions[Constants.PLAYER_MOVE_ACTION];
        jumpInputAction = playerInput.actions[Constants.PLAYER_JUMP_ACTION];
        attackInputAction = playerInput.actions[Constants.PLAYER_ATTACK_ACTION];
        dashInputAction = playerInput.actions[Constants.PLAYER_DASH_ACTION];
    }

    private void ReadInputs() {
        MoveInput = movementInputAction.ReadValue<float>();
        JumpInput = jumpInputAction.WasPressedThisFrame();
        AttackInput = attackInputAction.WasPressedThisFrame();
        DashInput = dashInputAction.WasPressedThisFrame();
    }
}