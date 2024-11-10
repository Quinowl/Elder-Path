using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Configurations/Player")]
public class PlayerConfiguration : ScriptableObject {
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float AccelerationRate { get; private set; }
    [field: SerializeField] public float DeccelerationRate { get; private set; }
    [field: SerializeField] public float JumpHeight { get; private set; }
    [field: SerializeField] public float JumpDuration { get; private set; }
    [field: SerializeField] public float GroundCheckDelayAfterJump { get; private set; }
    [field: HideInInspector] public float GravityForce { get; private set; }
    [field: HideInInspector] public float JumpForce { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }

    private void OnValidate() {
        GravityForce = -(2 * JumpHeight) / Mathf.Pow(JumpDuration, 2);
        JumpForce = Mathf.Sqrt(2 * Mathf.Abs(GravityForce) * JumpHeight);
    }
}