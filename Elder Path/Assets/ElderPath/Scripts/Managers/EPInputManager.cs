using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.InputSystem;

public class EPInputManager : MonoBehaviour {

    [SerializeField] private PlayerInput playerInput;

    private InputAction movementInputAction;

    public float MoveInput { get; private set; }
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
        // movementInputAction = playerInput.actions[Constants.PLAYER_MOVE_ACTION];
    }

    private void ReadInputs() {
        // MoveInput = movementInputAction.ReadValue<float>();
    }
}