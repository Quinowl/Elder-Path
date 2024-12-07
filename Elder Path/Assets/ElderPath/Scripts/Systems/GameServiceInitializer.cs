using UnityEngine;

public class GameServiceInitializer : MonoBehaviour {

    [SerializeField] private EPLevelManager levelManager;

    private void Start() {
        ServiceLocator.Instance.RegisterService(levelManager);
    }
}