using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Configurations/Player")]
public class PlayerConfiguration : ScriptableObject {
    [field: SerializeField] public float Speed {get; private set; }
}