using UnityEngine;

public class Lever : MonoBehaviour, IHittable, ISoundEmitter
{

    [SerializeField] private Sprite activatedSprite;
    [SerializeField] private Sprite unactivatedSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LeverWall[] lasersAssociated;

    private bool isActivated;

    public AudioClip AudioClip { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void OnDrawGizmos()
    {
        if (lasersAssociated == null || lasersAssociated.Length <= 0)
        {
            Debug.LogError("Lever has no walls associated.");
            return;
        }
        Gizmos.color = Color.grey;
        foreach (var wall in lasersAssociated)
        {
            Gizmos.DrawLine(transform.position, wall.transform.position);
        }
    }

    private void Awake()
    {
        if (!activatedSprite || !unactivatedSprite) Debug.LogError("Level has not sprites assigned.");
        if (!spriteRenderer) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        isActivated = false;
        spriteRenderer.sprite = unactivatedSprite;
    }

    public void Hit(HitContext context)
    {
        isActivated = !isActivated;
        ServiceLocator.Instance.GetService<EPSoundsManager>().PlaySFX(Constants.SFXIDs.MISC_LEVER_CHANGE_STATE, transform);
        spriteRenderer.sprite = isActivated ? activatedSprite : unactivatedSprite;
        foreach (var wall in lasersAssociated) wall.ChangeState();
    }
}