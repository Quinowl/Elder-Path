using System;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    public bool IsActive { get; private set; }

    public Action OnChangeState;

    private void OnTriggerEnter2D(Collider2D other) {
        ToggleActive(true);
    }

    private void OnTriggerExit2D(Collider2D other) {
        ToggleActive(false);
    }

    private void ToggleActive(bool active) {
        IsActive = active;
        OnChangeState?.Invoke();
    }
}