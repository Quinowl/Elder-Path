using System.Collections;
using UnityEngine;

public class EPLevelManager : MonoBehaviour {

    [SerializeField] private LevelManagerConfiguration configuration;
    [SerializeField] private Transform levelParent;

    private Level currentLevel;
    private Coroutine loadLevelCoroutine;

    private void Start() {
        if (!levelParent) levelParent = transform;
        LoadNextLevel();
    }

    [ContextMenu("Siguiente nivel")]
    public void LoadNextLevel() {
        int nextLevelIndex = currentLevel ? currentLevel.LevelIndex + 1 : 0;
        if (nextLevelIndex < 0 || nextLevelIndex >= configuration.Levels.Length) {
            if (nextLevelIndex < 0) Debug.LogError("Invalid level index");
            else Debug.Log("Fin del juego");
            return;
        }
        ServiceLocator.Instance.GetService<LoadingScreen>().StartLoading();
        if (loadLevelCoroutine != null) StopCoroutine(loadLevelCoroutine);
        loadLevelCoroutine = StartCoroutine(StartLoadLevel(nextLevelIndex));
    }

    private IEnumerator StartLoadLevel(int levelIndex) {
        float startTime = Time.time;
        UnloadCurrentLevel();
        currentLevel = Instantiate(configuration.Levels[levelIndex], levelParent);
        float elapsedTime = Time.time - startTime;
        if (elapsedTime < configuration.TransitionDuration) yield return new WaitForSeconds(configuration.TransitionDuration - elapsedTime);
        currentLevel.InitializeLevel();
        ServiceLocator.Instance.GetService<LoadingScreen>().StopLoading();
    }

    private void UnloadCurrentLevel() {
        if (currentLevel == null) return;
        Destroy(currentLevel.gameObject);
        currentLevel = null;
    }
}