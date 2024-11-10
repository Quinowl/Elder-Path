using System.Collections;
using UnityEngine;

public class EnemyDamageable : Damageable {

    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private bool canReceiveKnockback;
    [SerializeField] private float flashTime;
    [SerializeField] private Color flashColor;

    private MaterialPropertyBlock propertyBlock;
    private Coroutine colorFlashCoroutine;

    private void Awake() {
        if (!sprite) sprite = GetComponentInChildren<SpriteRenderer>();
        propertyBlock = new();
    }

    public override void TakeDamage(float damage) {
        if (canReceiveKnockback) ApplyKnockback();
        if (colorFlashCoroutine != null) StopCoroutine(colorFlashCoroutine);
        colorFlashCoroutine = StartCoroutine(HitAnimationCoroutine());
        base.TakeDamage(damage);
        // TODO: If I finally decide to put a life bar on the enemies, it should be updated here.
    }

    private void ApplyKnockback() {
        //TODO: Knockback
    }

    private IEnumerator HitAnimationCoroutine() {
        float elapsedTime = 0f;
        while (elapsedTime < flashTime) {
            elapsedTime += Time.deltaTime;
            float currentFlashAmount = Mathf.Lerp(1f, 0f, elapsedTime / flashTime);
            propertyBlock.SetColor("_Color", flashColor * currentFlashAmount);
            sprite.SetPropertyBlock(propertyBlock);
            yield return null;
        }
        propertyBlock.SetColor("_Color", Color.white);
        sprite.SetPropertyBlock(propertyBlock);        
    }
}