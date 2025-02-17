using System.Collections;
using UnityEngine;

public class EPLevelManager : MonoBehaviour
{

    [SerializeField] private LevelManagerConfiguration configuration;
    [SerializeField] private Transform levelParent;
    [SerializeField] private CameraRig cameraRig;

    private Level currentLevel;
    private Coroutine loadLevelCoroutine;

    private void Start()
    {
        if (!levelParent) levelParent = transform;
        LoadNextLevel();
    }

    [ContextMenu("Siguiente nivel")]
    public void LoadNextLevel()
    {
        int nextLevelIndex = currentLevel ? currentLevel.LevelIndex + 1 : 0;
        if (nextLevelIndex < 0 || nextLevelIndex >= configuration.Levels.Length)
        {
            if (nextLevelIndex < 0) Debug.LogError("Invalid level index");
            else ServiceLocator.Instance.GetService<EPGameManager>().EndGame();
            return;
        }
        ServiceLocator.Instance.GetService<LoadingScreen>().StartLoading();
        if (loadLevelCoroutine != null) StopCoroutine(loadLevelCoroutine);
        loadLevelCoroutine = StartCoroutine(StartLoadLevel(nextLevelIndex));
    }

    public void RestartLevelWithLoadingScreen()
    {
        ServiceLocator.Instance.GetService<LoadingScreen>().StartLoading();
        if (loadLevelCoroutine != null) StopCoroutine(loadLevelCoroutine);
        loadLevelCoroutine = StartCoroutine(StartLoadLevel(currentLevel.LevelIndex));
    }

    public void RestartLevelWithoutLoadingScreen()
    {
        int levelIndex = currentLevel.LevelIndex;
        UnloadCurrentLevel();
        currentLevel = Instantiate(configuration.Levels[levelIndex], levelParent);
        currentLevel.InitializeLevel();
    }

    private IEnumerator StartLoadLevel(int levelIndex)
    {
        float startTime = Time.time;
        UnloadCurrentLevel();
        currentLevel = Instantiate(configuration.Levels[levelIndex], levelParent);
        float elapsedTime = Time.time - startTime;
        if (elapsedTime < configuration.TransitionDuration) yield return new WaitForSeconds(configuration.TransitionDuration - elapsedTime);
        currentLevel.InitializeLevel();
        cameraRig.Configure(currentLevel.MovableCamera, currentLevel.MinCameraBounds, currentLevel.MaxCameraBounds);
        ServiceLocator.Instance.GetService<BirdsManager>().transform.ClearChilds();
        ServiceLocator.Instance.GetService<EPGameManager>().LevelChanged(levelIndex + 1, configuration.Levels.Length);
        ServiceLocator.Instance.GetService<LoadingScreen>().StopLoading();
    }

    private void UnloadCurrentLevel()
    {
        if (currentLevel == null) return;
        Destroy(currentLevel.gameObject);
        currentLevel = null;
    }
}