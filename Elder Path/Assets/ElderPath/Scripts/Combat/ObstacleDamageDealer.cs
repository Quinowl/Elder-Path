using System;
using UnityEngine;

public class ObstacleDamageDealer : MonoBehaviour {

    public Action OnCollision;

    private void OnCollisionEnter2D(Collision2D other) {
        OnCollision?.Invoke();
        if (other.gameObject.CompareTag(Constants.Tags.PLAYER)) ServiceLocator.Instance.GetService<EPLevelManager>().RestartLevelWithoutLoadingScreen();
    }
}