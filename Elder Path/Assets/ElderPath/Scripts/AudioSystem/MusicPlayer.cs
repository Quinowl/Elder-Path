using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeDuration;
    private Audio[] themes;
    private Coroutine transitionCoroutine;

    private void Awake() {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
        InitializeThemes();
    }

    private void InitializeThemes() {
        themes = Resources.LoadAll<Audio>("Audios/Music");
    }

    public void PlayMenuTheme() => PlayThemeByID("main-menu");

    public void PlayGameTheme() => PlayThemeByID("game");

    private void PlayThemeByID(string id) {
        Audio[] filteredAudios = themes.Where(t => t.ID == id).ToArray();
        if (filteredAudios == null || filteredAudios.Length <= 0) {
            Debug.LogError($"No audio theme found with '{id}' id");
            return;
        }
        AudioClip clipToPlay = filteredAudios[Random.Range(0, filteredAudios.Length)].GetRandomAudioClip();
        if (!clipToPlay) {
            Debug.LogError($"AudioClip with ID: {id} does not contain any valid clip.");
            return;
        }
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(StartFadeToClip(clipToPlay));
    }

    private IEnumerator StartFadeToClip(AudioClip newClip) {
        float initialVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime) {
            audioSource.volume = Mathf.Lerp(initialVolume, 0, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 0;

        audioSource.clip = newClip;
        audioSource.Play();

        for (float t = 0; t < fadeDuration; t += Time.deltaTime) {
            audioSource.volume = Mathf.Lerp(0, initialVolume, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = initialVolume;
    }
}