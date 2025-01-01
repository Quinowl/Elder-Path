using UnityEngine;

public class LevelRestartTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) ServiceLocator.Instance.GetService<EPLevelManager>().RestartLevelWithoutLoadingScreen();
    }
}