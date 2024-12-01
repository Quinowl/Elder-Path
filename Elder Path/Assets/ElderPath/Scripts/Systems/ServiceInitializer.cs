using UnityEngine;

public class ServiceInitializer : MonoBehaviour {

    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private SceneLoader sceneLoader;

    private void Start() {

        DontDestroyOnLoad(loadingScreen);
        DontDestroyOnLoad(sceneLoader);

        sceneLoader.Initialize(loadingScreen);

        ServiceLocator.Instance.RegisterService(loadingScreen);
        ServiceLocator.Instance.RegisterService(sceneLoader);
    }
}