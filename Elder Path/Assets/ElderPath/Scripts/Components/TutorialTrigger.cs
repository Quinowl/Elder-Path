using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : ReactiveUI
{

    [SerializeField] private CanvasGroup prompt;
    [SerializeField] private Image promptImage;
    [SerializeField] private Sprite keyboardSprite;
    [SerializeField] private Sprite gamepadSprite;

    private void Awake()
    {
        if (!keyboardSprite || !gamepadSprite) Debug.LogWarning("Sprites to change are not configurated.");
    }

    protected override void Start()
    {
        base.Start();
        promptImage.sprite = EPInputManager.Instance.IsGamepad ? gamepadSprite : keyboardSprite;
        prompt.Toggle(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER)) SetPromptVisibility(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER)) SetPromptVisibility(false);
    }

    private void SetPromptVisibility(bool isVisible) => prompt.Toggle(isVisible);

    protected override void OnInputDeviceChange() => promptImage.sprite = EPInputManager.Instance.IsGamepad ? gamepadSprite : keyboardSprite;
}