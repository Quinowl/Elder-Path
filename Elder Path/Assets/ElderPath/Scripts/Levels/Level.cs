using System;
using NaughtyAttributes;
using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField] private FinalLevelTrigger finalTrigger;
    [field: SerializeField] public int LevelIndex { get; private set; }
    [field: SerializeField] public bool MovableCamera { get; private set; }
    [field: SerializeField, ShowIf(nameof(MovableCamera))] public Vector2 MinCameraBounds { get; private set; } = -Vector2.one;
    [field: SerializeField, ShowIf(nameof(MovableCamera))] public Vector2 MaxCameraBounds { get; private set; } = Vector2.one;
    [field: SerializeField] public Transform InitialPlayerPosition { get; private set; }
    public static Action<Level> OnLevelLoaded;

    private void OnDrawGizmos() {
        if (MovableCamera) {

            Gizmos.color = Color.green;

            Vector3 bottomLeft = new Vector3(MinCameraBounds.x, MinCameraBounds.y, 0f);
            Vector3 bottomRight = new Vector3(MaxCameraBounds.x, MinCameraBounds.y, 0f);
            Vector3 topLeft = new Vector3(MinCameraBounds.x, MaxCameraBounds.y, 0f);
            Vector3 topRight = new Vector3(MaxCameraBounds.x, MaxCameraBounds.y, 0f);

            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.DrawLine(bottomLeft, topLeft);
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
        }
    }

    public void InitializeLevel() {
        OnLevelLoaded?.Invoke(this);
        finalTrigger.Configure(this);
    }

    public void EndLevel() => ServiceLocator.Instance.GetService<EPLevelManager>().LoadNextLevel();
}