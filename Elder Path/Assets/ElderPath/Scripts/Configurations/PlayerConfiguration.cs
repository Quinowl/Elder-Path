using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Configurations/Player")]
public class PlayerConfiguration : ScriptableObject {
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float AccelerationRate { get; private set; }
    [field: SerializeField] public float DeccelerationRate { get; private set; }
    [field: SerializeField] public float PushingSpeed { get; private set; }
    [field: SerializeField] public float JumpHeight { get; private set; }
    [field: SerializeField] public float JumpDuration { get; private set; }
    [field: SerializeField] public float MaxFalligSpeed { get; private set; }
    [field: HideInInspector] public float GravityForce { get; private set; }
    [field: HideInInspector] public float JumpForce { get; private set; }
    [field: SerializeField] public float CoyoteTime { get; private set; }
    [field: SerializeField] public float DashForce { get; private set; }
    [field: SerializeField] public float DashTime { get; private set; }
    [field: SerializeField] public float DashCooldown { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float AttackDamage { get; private set; }

    private void OnValidate() {
        CalculateValues();
    }

    public void CalculateValues() {
        GravityForce = -(2 * JumpHeight) / Mathf.Pow(JumpDuration, 2);
        JumpForce = Mathf.Sqrt(2 * Mathf.Abs(GravityForce) * JumpHeight);
    }
}