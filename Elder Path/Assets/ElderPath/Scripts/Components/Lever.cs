using UnityEngine;

public class Lever : MonoBehaviour, IHittable {

    [SerializeField] private Sprite activatedSprite;
    [SerializeField] private Sprite unactivatedSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LeverWall[] wallsAssociated;

    private bool isActivated;

    private void OnDrawGizmos() {
        if (wallsAssociated == null || wallsAssociated.Length <= 0) {
            Debug.LogError("Lever has no walls associated.");
            return;
        }
        Gizmos.color = Color.grey;
        foreach (var wall in wallsAssociated) {
            Gizmos.DrawLine(transform.position, wall.transform.position);
        }
    }

    private void Awake() {
        if (!activatedSprite || !unactivatedSprite) Debug.LogError("Level has not sprites assigned.");
        if (!spriteRenderer) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        isActivated = false;
        spriteRenderer.sprite = unactivatedSprite;
        //TODO: Modificar el sprite de activado para que se vea cuándo lo está y cuando no
    }

    public void Hit(HitContext context) {
        isActivated = !isActivated;
        spriteRenderer.sprite = isActivated ? activatedSprite : unactivatedSprite;
        foreach (var wall in wallsAssociated) wall.ChangeState();
    }
}