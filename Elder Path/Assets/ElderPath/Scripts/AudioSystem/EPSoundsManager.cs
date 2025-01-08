using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EPSoundsManager : MonoBehaviour
{

    [SerializeField] private AudioPlayer audioPlayerPrefab;
    [SerializeField] private int initialPoolSize;

    private Dictionary<string, Audio> audioDictionary;

    public ObjectPool<AudioPlayer> AudioPlayerPool { get; private set; }

    private void Start()
    {
        AudioPlayerPool = new ObjectPool<AudioPlayer>(audioPlayerPrefab, initialPoolSize, transform);
        InitializeAudios();
    }

    public void PlaySFX(string sfxID, Transform spawnTransform)
    {
        if (!audioDictionary.TryGetValue(sfxID, out Audio audio))
        {
            Debug.LogWarning($"SFX ID: '{sfxID}' not found.");
            return;
        }
        AudioClip clip = audio.Clips[Random.Range(0, audio.Clips.Length)];
        AudioPlayer audioPlayer = AudioPlayerPool.Get();
        audioPlayer.transform.position = spawnTransform.position;
        audioPlayer.PlayClip(clip);
    }

    public void PlayClip(AudioClip clip, Transform spawnTransform)
    {
        AudioPlayer audioPlayer = AudioPlayerPool.Get();
        audioPlayer.transform.position = spawnTransform.position;
        audioPlayer.PlayClip(clip);
    }

    private void InitializeAudios()
    {
        Audio[] audios = Resources.LoadAll<Audio>("Audios/SFXs");
        audioDictionary = audios.ToDictionary(a => a.ID);
    }
}