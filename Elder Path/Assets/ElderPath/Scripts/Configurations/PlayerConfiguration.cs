using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Scriptable Objects/Configurations/Player")]
public class PlayerConfiguration : ScriptableObject {
    [field: SerializeField, BoxGroup("Movement")] public float MaxSpeed { get; private set; }
    [field: SerializeField, BoxGroup("Movement")] public float MinSpeedOnDirectionChange { get; private set; }
    [field: SerializeField, BoxGroup("Movement")] public float AccelerationRate { get; private set; }
    [field: SerializeField, BoxGroup("Movement")] public float DeccelerationRate { get; private set; }
    [field: SerializeField, BoxGroup("Push")] public float PushingSpeed { get; private set; }
    [field: SerializeField, BoxGroup("Jump")] public float JumpHeight { get; private set; }
    [field: SerializeField, BoxGroup("Jump")] public float JumpDuration { get; private set; }
    [field: SerializeField, BoxGroup("Jump")] public float MaxFalligSpeed { get; private set; }
    [field: HideInInspector] public float GravityForce { get; private set; }
    [field: HideInInspector] public float JumpForce { get; private set; }
    [field: SerializeField, BoxGroup("Jump")] public float CoyoteTime { get; private set; }
    [field: SerializeField, BoxGroup("Dash")] public float DashForce { get; private set; }
    [field: SerializeField, BoxGroup("Dash")] public float DashTime { get; private set; }
    [field: SerializeField, BoxGroup("Dash")] public float DashCooldown { get; private set; }
    [field: SerializeField, BoxGroup("Dash")] public PlayerTrail TrailPrefab { get; private set; }
    [field: SerializeField, BoxGroup("Dash")] public int TrailPoolInitialSize { get; private set; }
    [field: SerializeField, BoxGroup("Dash")] public float TrailSpawnDistance { get; private set; }
    [field: SerializeField, BoxGroup("Attack")] public float AttackRange { get; private set; }
    [field: SerializeField, BoxGroup("Attack")] public float AttackDamage { get; private set; }
    [field: SerializeField, BoxGroup("Attack")] public float AttackTime { get; private set; }
    [field: SerializeField, BoxGroup("Attack")] public float AttackCooldown { get; private set; }

    private void OnValidate() {
        CalculateValues();
    }

    public void CalculateValues() {
        GravityForce = -(2 * JumpHeight) / Mathf.Pow(JumpDuration, 2);
        JumpForce = Mathf.Sqrt(2 * Mathf.Abs(GravityForce) * JumpHeight);
    }
}