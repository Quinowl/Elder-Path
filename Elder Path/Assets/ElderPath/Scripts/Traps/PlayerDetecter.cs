using System;
using UnityEngine;

public class PlayerDetecter : MonoBehaviour
{

    public Action OnPlayerDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER)) OnPlayerDetected?.Invoke();
    }
}