using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour {

    [Header("Audio settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [Header("Display settings")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] availableResolutions;

    public void InitializeSettings() {
        InitializeAudioSettings();
    }

    private void InitializeAudioSettings() {
        masterSlider.value = PlayerPrefs.GetFloat(Constants.SaveKeys.MASTER_VOLUME, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(Constants.SaveKeys.SFX_VOLUME, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(Constants.SaveKeys.MUSIC_VOLUME, 1f);

        SetMasterVolume(masterSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetMusicVolume(musicSlider.value);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void SetMasterVolume(float volume) => SetAudioMixerVolume("masterVolume", volume, Constants.SaveKeys.MASTER_VOLUME);

    private void SetSFXVolume(float volume) => SetAudioMixerVolume("sfxVolume", volume, Constants.SaveKeys.SFX_VOLUME);

    private void SetMusicVolume(float volume) => SetAudioMixerVolume("musicVolume", volume, Constants.SaveKeys.MUSIC_VOLUME);

    private void SetAudioMixerVolume(string mixerParemeter, float volume, string saveKey) {
        if (!audioMixer) {
            Debug.LogError("Audio Mixer is not assigned.");
            return;
        }
        audioMixer.SetFloat(mixerParemeter, volume > 0.0001f ? Mathf.Log10(volume) * 20 : -80f);
        PlayerPrefs.SetFloat(saveKey, volume);
    }
}