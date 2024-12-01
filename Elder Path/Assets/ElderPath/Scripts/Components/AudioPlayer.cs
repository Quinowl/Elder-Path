using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;

    private float lifeTime;

    private void Awake() {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Time.time > lifeTime) ServiceLocator.Instance.GetService<SoundsPlayer>().AudioPlayerPool.ReturnToPool(this);
    }

    public void Initialize(AudioClip audioClip, float volume) {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        lifeTime = audioSource.clip.length + Time.time;
    }
}