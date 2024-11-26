using UnityEngine;

public class FinalLevelTrigger : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Final del nivel");
        }
    }
}
