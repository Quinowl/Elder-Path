using UnityEngine;

public class LeverWall : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer laserRenderer;
    [SerializeField] private Collider2D laserCollider;
    [SerializeField] private Animator animator;
    [Header("Configuration")]
    [SerializeField, Range(0f, 1f)] private float inactiveAlpha = 0.3f;
    [SerializeField] private bool isActiveAtStart = true;

    private bool isActive;

    private void Awake()
    {
        if (!laserRenderer) Debug.LogError("No laser renderer assigned.");
        if (!laserCollider) Debug.LogError("No laser collider assigned.");
        if (!animator) animator.GetComponent<Animator>();
        isActive = isActiveAtStart;
        laserCollider.enabled = isActive;
        animator.Play(isActive ? Constants.MiscAnimations.LASER_ENABLE : Constants.MiscAnimations.LASER_DISABLE);
        SetRendererAlpha(isActive ? 1f : inactiveAlpha);
    }

    public void ChangeState()
    {
        isActive = !isActive;
        laserCollider.enabled = isActive;
        animator.Play(isActive ? Constants.MiscAnimations.LASER_ENABLE : Constants.MiscAnimations.LASER_DISABLE);
        SetRendererAlpha(isActive ? 1f : 0.3f);
    }

    private void SetRendererAlpha(float nextAlpha)
    {
        Color rendererColor = laserRenderer.color;
        rendererColor.a = nextAlpha;
        laserRenderer.color = rendererColor;
    }
}