using UnityEngine;
using TMPro;

public class GameView : MonoBehaviour {

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private TMP_Text levelText;

    private GameMenuMediator gameMenuMediator;

    public void Configure(GameMenuMediator mediator) => gameMenuMediator = mediator;

    public void UpdateLevelText(int currentLevel, int totalLevels) => gameMenuMediator.UpdateText(levelText, $"{currentLevel} / {totalLevels}");

}