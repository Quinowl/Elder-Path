using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;

    private float lifeTime;

    private void Awake() {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Time.time > lifeTime) ServiceLocator.Instance.GetService<EPSoundsManager>().AudioPlayerPool.ReturnToPool(this);
    }

    public void PlayClip(AudioClip audioClip) {
        audioSource.clip = audioClip;
        audioSource.Play();
        lifeTime = audioSource.clip.length + Time.time;
    }
}