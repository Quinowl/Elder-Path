using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Audio")]
public class Audio : ScriptableObject {
    [field: SerializeField] public string ID { get; private set; }
    [field: SerializeField] public AudioClip[] Clips { get; private set; }
}