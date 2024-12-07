using System;
using UnityEngine;

public class Level : MonoBehaviour {
    [field: SerializeField] public int LevelIndex { get; private set; }
    [field: SerializeField] public Transform InitialPlayerPosition { get; private set; }
    public static Action<Level> OnLevelLoaded;

    public void InitializeLevel() => OnLevelLoaded?.Invoke(this);
}