using UnityEngine;
using TMPro;

public class GameView : MonoBehaviour {

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private TMP_Text levelText;

    private GameMenuMediator gameMenuMediator;

    private void OnEnable() {
        EPGameManager.OnLevelChanged += UpdateLevelText;
    }

    private void OnDisable() {
        EPGameManager.OnLevelChanged -= UpdateLevelText;
    }

    public void Configure(GameMenuMediator mediator) => gameMenuMediator = mediator;

    public void Show() => canvasGroup.Toggle(true);

    public void Hide() => canvasGroup.Toggle(false);

    public void UpdateLevelText((int currentLevel, int totalLevels) levelInfo) => gameMenuMediator.UpdateText(levelText, $"{levelInfo.currentLevel} / {levelInfo.totalLevels}");

}