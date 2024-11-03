using UnityEngine;
using NaughtyAttributes;

public class AreaChecker : MonoBehaviour {

    [System.Serializable]
    private enum AreaCheckerForm {
        Circle,
        Square
    }

    [SerializeField] private AreaCheckerForm form;
    [SerializeField] private bool drawGizmo;
    [SerializeField] private Color gizmoColor;
    private bool IsSquare => form == AreaCheckerForm.Square;
    private bool IsCircle => form == AreaCheckerForm.Circle;
    [SerializeField, ShowIf(nameof(IsCircle))] private float checkRadius;
    [SerializeField, ShowIf(nameof(IsSquare))] private Vector3 checkSize;
    [SerializeField] private LayerMask collisionLayer;

    private void OnDrawGizmos() {
        
        if (!drawGizmo) return;
        Gizmos.color = gizmoColor;
        switch (form) {
            case AreaCheckerForm.Circle:
                Gizmos.DrawWireSphere(transform.position, checkRadius);
            break;
            case AreaCheckerForm.Square:
                Gizmos.DrawWireCube(transform.position, checkSize);
            break;
        }
    }

    public bool IsOverlapping() {
        switch (form) {
            case AreaCheckerForm.Circle:
                return Physics2D.OverlapCircle(transform.position, checkRadius, collisionLayer);
            case AreaCheckerForm.Square:
                return Physics2D.OverlapBox(transform.position, checkSize, 0f, collisionLayer);
            default:
                return false;
        }
    }   
}