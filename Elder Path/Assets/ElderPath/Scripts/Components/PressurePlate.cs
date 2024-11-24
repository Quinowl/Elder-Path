using System;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    public Action OnChangeState;
    public bool IsActive { get; private set; }
    private readonly List<string> validTags = new List<string> { "Player", "Rock" };
    private List<Collider2D> validObjectsOnPlate = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D other) {
        if (IsValidObject(other) && !validObjectsOnPlate.Contains(other)) {
            validObjectsOnPlate.Add(other);
            UpdateState();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (validObjectsOnPlate.Contains(other)) {
            validObjectsOnPlate.Remove(other);
            UpdateState();
        }
    }

    private void UpdateState() {
        bool newState = validObjectsOnPlate.Count > 0;
        if (newState != IsActive) {
            IsActive = newState;
            OnChangeState?.Invoke();
        }
    }

    private bool IsValidObject(Collider2D collider2D) => validTags.Contains(collider2D.tag);
}