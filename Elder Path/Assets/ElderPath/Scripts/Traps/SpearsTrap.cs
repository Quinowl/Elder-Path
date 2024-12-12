using UnityEngine;

public class SpearsTrap : MonoBehaviour {

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite fallingSprite;

    private void Awake() {
        if (!idleSprite || !fallingSprite) Debug.LogError("Idle sprite or falling sprite is not configured.");
        if (!spriteRenderer) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite;
    }
}