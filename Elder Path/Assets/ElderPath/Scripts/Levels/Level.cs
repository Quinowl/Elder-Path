using System;
using UnityEngine;

public class Level : MonoBehaviour {
    [SerializeField] private FinalLevelTrigger finalTrigger;
    [field: SerializeField] public int LevelIndex { get; private set; }
    [field: SerializeField] public Transform InitialPlayerPosition { get; private set; }
    public static Action<Level> OnLevelLoaded;

    public void InitializeLevel() {
        OnLevelLoaded?.Invoke(this);
        finalTrigger.Configure(this);
    }

    public void EndLevel() => ServiceLocator.Instance.GetService<EPLevelManager>().LoadNextLevel();
}