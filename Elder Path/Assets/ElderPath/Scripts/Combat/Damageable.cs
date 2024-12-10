using UnityEngine;

public abstract class Damageable : MonoBehaviour {

    protected float maxHeath;
    protected float currentHealth;

    public virtual void SetHealth(float health) {
        maxHeath = health;
        currentHealth = maxHeath;
    }

    public virtual void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0f) Die();
    }

    public virtual void Heal(float amount) {
        if (currentHealth >= maxHeath) return;
        currentHealth = Mathf.Min(maxHeath, currentHealth + amount);
    }

    protected void Die() => DieEffects();

    protected virtual void DieEffects() {

    }
}