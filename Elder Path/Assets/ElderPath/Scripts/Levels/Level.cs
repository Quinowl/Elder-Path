using System;
using UnityEngine;

public class Level : MonoBehaviour {
    [field: SerializeField] public int LevelIndex { get; private set; }
    [field: SerializeField] public Transform InitialPlayerPosition { get; private set; }
    public static Action<Vector3> OnLevelLoaded;

    public void InitializeLevel() {
        // TODO: Move the player to him initial level position
        OnLevelLoaded?.Invoke(InitialPlayerPosition.position);
    }
}