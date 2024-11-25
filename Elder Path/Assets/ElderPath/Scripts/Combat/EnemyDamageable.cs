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
        propertyBlock.SetColor("_Color", flashColor);
        sprite.SetPropertyBlock(propertyBlock);
        colorFlashCoroutine = StartCoroutine(HitAnimationCoroutine());
        base.TakeDamage(damage);
        // TODO: If I finally decide to put a life bar on the enemies, it should be updated here.
    }

    private void ApplyKnockback() {
        //TODO: Knockback
    }

    private IEnumerator HitAnimationCoroutine() {
        float elapsedTime = 0f;
        Color startColor = propertyBlock.GetColor("_Color");
        while (elapsedTime < flashTime) {
            Color currentColor = Color.Lerp(startColor, Color.white, elapsedTime / flashTime);
            propertyBlock.SetColor("_Color", currentColor);
            sprite.SetPropertyBlock(propertyBlock);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        propertyBlock.SetColor("_Color", Color.white);
        sprite.SetPropertyBlock(propertyBlock);
    }
}