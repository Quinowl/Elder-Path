using UnityEngine;
using UnityEngine.UI;

public class EndGameView : MonoBehaviour {

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private Button menuButton;

    private GameMenuMediator gameMenuMediator;

    public void Configure(GameMenuMediator mediator) => gameMenuMediator = mediator;

    private void Start() {
        InitializeButtons();
    }

    public void Show() {
        canvasGroup.Toggle(true);
        gameMenuMediator.OnUpdateSelectedObjectInEventSystem(menuButton.gameObject);
    }

    public void Hide() => canvasGroup.Toggle(false);

    private void InitializeButtons() {
        menuButton.onClick.RemoveAllListeners();
        menuButton.onClick.AddListener(OnMenuButton);
    }

    private void OnMenuButton() => gameMenuMediator.OnBackToMenuButtonPressed();
}
