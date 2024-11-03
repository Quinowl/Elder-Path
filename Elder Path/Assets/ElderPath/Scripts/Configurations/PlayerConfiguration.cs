using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Configurations/Player")]
public class PlayerConfiguration : ScriptableObject {
    [field: SerializeField] public float MaxSpeed {get; private set; }
    [field: SerializeField] public float AccelerationRate { get; private set; }
    [field: SerializeField] public float DeccelerationRate { get; private set; }
}