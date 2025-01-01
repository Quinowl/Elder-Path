using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Audio")]
public class Audio : ScriptableObject
{
    [field: SerializeField] public string ID { get; private set; }
    [field: SerializeField] public AudioClip[] Clips { get; private set; }
    public AudioClip GetRandomAudioClip()
    {
        if (Clips == null || Clips.Length <= 0)
        {
            Debug.LogError($"There is not clip associated in {name}");
            return default;
        }
        return Clips[Random.Range(0, Clips.Length)];
    }
}