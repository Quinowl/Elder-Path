using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    private LoadingScreen loadingScreen;

    public void Initialize(LoadingScreen loadingScreen) => this.loadingScreen = loadingScreen;

    public void LoadScene(string sceneName, float duration = 1f) {
        if (loadingScreen == null) {
            Debug.LogWarning("No LoadingScreen registered in SceneLoader.");
            return;
        }
        StartCoroutine(StartLoadScene(sceneName, duration));
    }

    private IEnumerator StartLoadScene(string sceneName, float duration) {
        loadingScreen.StartLoading();
        float finalTime = Time.time + duration;
        try {
            SceneManager.LoadScene(sceneName);
        }
        catch (Exception e) {
            Debug.LogError($"Failed to load scene {sceneName}: {e.Message}");
        }
        if (finalTime > Time.time) yield return new WaitForSeconds(finalTime - Time.time);
        if (loadingScreen.gameObject.activeSelf) loadingScreen.StopLoading();
    }
}