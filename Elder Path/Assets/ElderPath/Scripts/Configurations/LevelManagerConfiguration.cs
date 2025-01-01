using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Configurations/LevelManager")]
public class LevelManagerConfiguration : ScriptableObject
{
    [field: SerializeField] public Level[] Levels { get; private set; }
    [field: SerializeField] public float TransitionDuration { get; private set; }
}