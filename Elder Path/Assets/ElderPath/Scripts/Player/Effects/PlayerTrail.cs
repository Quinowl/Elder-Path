using UnityEngine;

public class PlayerTrail : MonoBehaviour {

    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Settings")]
    [SerializeField] private float activeTime = 0.1f;
    [SerializeField] private float initialAlpha = 0.8f;
    [SerializeField] private float alphaDecayRate = 0.95f;

    private float timeActivated;
    private PlayerController player;
    private MaterialPropertyBlock propertyBlock;

    public void Initialize(PlayerController player) {
        this.player = player;

        spriteRenderer.sprite = player.SpriteRenderer.sprite;
        transform.position = player.transform.position;
        transform.localScale = player.transform.localScale;
        timeActivated = Time.time;

        propertyBlock = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(propertyBlock);

        propertyBlock.SetColor("_Color", new Color(1f, 1f, 1f, initialAlpha));
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }

    private void Update() {
        UpdateAlpha();
        CheckActiveTime();
    }

    private void UpdateAlpha() {
        spriteRenderer.GetPropertyBlock(propertyBlock);
        Color currentColor = propertyBlock.GetColor("_Color");
        currentColor.a *= alphaDecayRate;
        propertyBlock.SetColor("_Color", currentColor);
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }

    private void CheckActiveTime() {
        if (Time.time >= (timeActivated + activeTime)) player.TrailPool.ReturnToPool(this);
    }
}