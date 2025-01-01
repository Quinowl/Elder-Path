using UnityEngine;

public interface IHittable
{
    public abstract void Hit(HitContext context);
}

public class HitContext
{
    public float Damage { get; }
    public Vector2 HitDirection { get; }
    public HitContext(float damage, Vector2 hitDirection)
    {
        Damage = damage;
        HitDirection = hitDirection;
    }
}