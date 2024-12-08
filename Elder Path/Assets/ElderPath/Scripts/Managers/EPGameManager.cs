using System;
using UnityEngine;

public class EPGameManager : MonoBehaviour {

    public static Action OnEndGame;

    public void EndGame() {
        OnEndGame?.Invoke();
    }
}