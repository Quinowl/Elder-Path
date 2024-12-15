using UnityEngine;

public class ServiceInitializer : MonoBehaviour {

    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private EPSoundsManager soundsPlayer;
    [SerializeField] private MusicPlayer musicPlayer;

    private void Start() {

        DontDestroyOnLoad(loadingScreen);
        DontDestroyOnLoad(sceneLoader);
        DontDestroyOnLoad(soundsPlayer);
        DontDestroyOnLoad(musicPlayer);

        sceneLoader.Initialize(loadingScreen);

        ServiceLocator.Instance.RegisterService(loadingScreen);
        ServiceLocator.Instance.RegisterService(sceneLoader);
        ServiceLocator.Instance.RegisterService(soundsPlayer);
        ServiceLocator.Instance.RegisterService(musicPlayer);
    }
}