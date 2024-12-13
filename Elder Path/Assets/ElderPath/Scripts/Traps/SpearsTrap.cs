using UnityEngine;

public class SpearsTrap : MonoBehaviour {

    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerDetecter playerDetecter;

    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite fallingSprite;

    private void Awake() {
        if (!idleSprite || !fallingSprite) Debug.LogError("Idle sprite or falling sprite is not configured.");
        if (!spriteRenderer) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite;
    }

    private void Start() {
        playerDetecter.OnPlayerDetected += FallSpears;
    }

    private void OnDestroy() {
        playerDetecter.OnPlayerDetected -= FallSpears;
    }

    private void FallSpears() {
        Debug.Log("Player detected");
    }
}