using UnityEngine;

public class GameServiceInitializer : MonoBehaviour
{

    [SerializeField] private EPLevelManager levelManager;
    [SerializeField] private EPGameManager gameManager;

    private void Start()
    {
        ServiceLocator.Instance.RegisterService(levelManager);
        ServiceLocator.Instance.RegisterService(gameManager);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<EPLevelManager>();
        ServiceLocator.Instance.UnregisterService<EPGameManager>();
    }
}