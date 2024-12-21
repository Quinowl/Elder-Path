using System;
using UnityEngine;

public class EPGameManager : MonoBehaviour {

    public static Action OnEndGame;
    public static Action<(int, int)> OnLevelChanged;

    public void EndGame() => OnEndGame?.Invoke();

    public void LevelChanged(int currentLevel, int totalLevels) => OnLevelChanged?.Invoke((currentLevel, totalLevels));
}