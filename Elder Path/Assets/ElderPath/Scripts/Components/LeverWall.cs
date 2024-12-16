using UnityEngine;

public class LeverWall : MonoBehaviour {

    [SerializeField] private SpriteRenderer laserRenderer;
    [SerializeField] private Collider2D laserCollider;

    private bool isActive;

    private void Awake() {
        if (!laserRenderer) Debug.LogError("No laser renderer assigned.");
        SetRendererAlpha(1f);
        isActive = true;
    }

    public void ChangeState() {
        isActive = !isActive;
        laserCollider.enabled = isActive;
        SetRendererAlpha(isActive ? 1f : 0.3f);
    }

    private void SetRendererAlpha(float nextAlpha) {
        Color rendererColor = laserRenderer.color;
        rendererColor.a = nextAlpha;
        laserRenderer.color = rendererColor;
    }
}