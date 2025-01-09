using UnityEngine;

public class GameServiceInitializer : MonoBehaviour
{

    [SerializeField] private EPLevelManager levelManager;
    [SerializeField] private EPGameManager gameManager;
    [SerializeField] private BirdsManager birdsManager;

    private void Start()
    {
        ServiceLocator.Instance.RegisterService(levelManager);
        ServiceLocator.Instance.RegisterService(gameManager);
        ServiceLocator.Instance.RegisterService(birdsManager);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<EPLevelManager>();
        ServiceLocator.Instance.UnregisterService<EPGameManager>();
        ServiceLocator.Instance.UnregisterService<BirdsManager>();
    }
}