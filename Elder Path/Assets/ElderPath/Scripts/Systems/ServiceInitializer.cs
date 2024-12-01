using UnityEngine;

public class ServiceInitializer : MonoBehaviour {

    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private SoundsPlayer soundsPlayer;

    private void Start() {

        DontDestroyOnLoad(loadingScreen);
        DontDestroyOnLoad(sceneLoader);
        DontDestroyOnLoad(soundsPlayer);

        sceneLoader.Initialize(loadingScreen);

        ServiceLocator.Instance.RegisterService(loadingScreen);
        ServiceLocator.Instance.RegisterService(sceneLoader);
        ServiceLocator.Instance.RegisterService(soundsPlayer);
    }
}