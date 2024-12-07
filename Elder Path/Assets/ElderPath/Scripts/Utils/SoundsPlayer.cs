using UnityEngine;

public class SoundsPlayer : MonoBehaviour {

    [SerializeField] private AudioPlayer audioPlayerPrefab;
    [SerializeField] private int initialPoolSize;

    public ObjectPool<AudioPlayer> AudioPlayerPool { get; private set; }

    private void Start() {
        AudioPlayerPool = new ObjectPool<AudioPlayer>(audioPlayerPrefab, initialPoolSize, transform);
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume) {
        if (audioClip == null) {
            Debug.LogWarning("Attempted to play a null AudioClip.");
            return;
        }
        AudioPlayer audioPlayer = AudioPlayerPool.Get();
        audioPlayer.transform.position = spawnTransform.position;
        audioPlayer.Initialize(audioClip, volume);
    }
}