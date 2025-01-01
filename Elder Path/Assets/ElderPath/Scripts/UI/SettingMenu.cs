using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;

public class SettingMenu : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Button closeButton;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Audio settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [Header("Display settings")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    public GameObject FirstSelectedObject => masterSlider.gameObject;

    private Resolution[] availableResolutions;

    public void InitializeSettings()
    {
        InitializeAudioSettings();
        InitializeDisplaySettings();
    }

    public void SetListenersToCloseButton(UnityAction listener)
    {
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(listener);
    }

    public void Hide() => canvasGroup.Toggle(false);

    public void Show() => canvasGroup.Toggle(true);

    private void InitializeAudioSettings()
    {
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

    private void SetAudioMixerVolume(string mixerParemeter, float volume, string saveKey)
    {
        if (!audioMixer)
        {
            Debug.LogError("Audio Mixer is not assigned.");
            return;
        }
        audioMixer.SetFloat(mixerParemeter, volume > 0.0001f ? Mathf.Log10(volume) * 20 : -80f);
        PlayerPrefs.SetFloat(saveKey, volume);
    }

    private void InitializeDisplaySettings()
    {
        InitializeResolutions();

        int savedResolutionIndex = PlayerPrefs.GetInt(Constants.SaveKeys.RESOLUTION, availableResolutions.Length - 1);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        ApplyResolution(savedResolutionIndex);

        bool isFullscreen = PlayerPrefs.GetInt(Constants.SaveKeys.FULLSCREEN, 1) == 1;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;

        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        resolutionDropdown.onValueChanged.AddListener(ApplyResolution);
    }

    private void InitializeResolutions()
    {
        availableResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string resolutionOption = $"{availableResolutions[i].width} x {availableResolutions[i].height}";
            options.Add(resolutionOption);

            if (availableResolutions[i].width == Screen.currentResolution.width &&
                availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void ApplyResolution(int resolutionIndex)
    {
        if (resolutionIndex < 0 || resolutionIndex >= availableResolutions.Length) return;
        Resolution selectedResolution = availableResolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, selectedResolution.refreshRateRatio);
        PlayerPrefs.SetInt(Constants.SaveKeys.RESOLUTION, resolutionIndex);
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(Constants.SaveKeys.FULLSCREEN, isFullscreen ? 1 : 0);
    }
}