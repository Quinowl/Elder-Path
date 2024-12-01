using UnityEngine;

public class ServiceInitializer : MonoBehaviour {

    [SerializeField] private LoadingScreen loadingScreen;

    private void Start() {
        DontDestroyOnLoad(loadingScreen);
        ServiceLocator.Instance.RegisterService(loadingScreen);
    }
}