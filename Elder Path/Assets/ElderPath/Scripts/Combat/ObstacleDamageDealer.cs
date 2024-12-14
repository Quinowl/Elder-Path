using System;
using UnityEngine;

public class ObstacleDamageDealer : MonoBehaviour {

    public Action<Vector3> OnCollision;

    private void OnCollisionEnter2D(Collision2D other) {
        OnCollision?.Invoke(other.contacts[0].point);
        if (other.gameObject.CompareTag(Constants.Tags.PLAYER))
            ServiceLocator.Instance.GetService<EPLevelManager>().RestartLevelWithoutLoadingScreen();
    }
}