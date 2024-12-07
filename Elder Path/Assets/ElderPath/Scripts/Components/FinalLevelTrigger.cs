using UnityEngine;

public class FinalLevelTrigger : MonoBehaviour {

    private Level currentLevel;

    public void Configure(Level level) => currentLevel = level;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) currentLevel.EndLevel();
    }
}
