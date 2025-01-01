using UnityEngine;

public class Wall : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private PressurePlate[] associatedPlates;
    [SerializeField] private bool drawGizmo = true;

    private bool isOpening;
    private float animationProgress;

    private void OnDrawGizmos()
    {
        if (!drawGizmo) return;
        if (associatedPlates == null || associatedPlates.Length == 0) return;
        Gizmos.color = Color.cyan;
        foreach (var plate in associatedPlates) Gizmos.DrawLine(transform.position, plate.transform.position);
    }

    private void Awake()
    {
        foreach (var plate in associatedPlates) plate.OnChangeState += CheckOpenWall;
    }

    private void Update()
    {
        UpdateAnimationProgress();
    }

    private void OnDestroy()
    {
        foreach (var plate in associatedPlates) plate.OnChangeState -= CheckOpenWall;
    }

    private void CheckOpenWall()
    {
        bool allActive = AllPlatesActive();
        isOpening = allActive;
        collider2d.enabled = !allActive;
    }

    private void UpdateAnimationProgress()
    {
        if (isOpening) animationProgress += Time.deltaTime / animator.GetCurrentAnimatorStateInfo(0).length;
        else animationProgress -= Time.deltaTime / animator.GetCurrentAnimatorStateInfo(0).length;
        animationProgress = Mathf.Clamp01(animationProgress);
        if (animationProgress <= 0f) animator.Play(Constants.MiscAnimations.WALL_CLOSED);
        else if (animationProgress >= 1f) animator.Play(Constants.MiscAnimations.WALL_OPENING);
        else animator.Play(Constants.MiscAnimations.WALL_OPENING, 0, animationProgress);
    }

    private bool AllPlatesActive()
    {
        foreach (var plate in associatedPlates)
        {
            if (!plate.IsActive) return false;
        }
        return true;
    }
}