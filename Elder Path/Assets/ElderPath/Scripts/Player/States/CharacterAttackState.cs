using System.Linq;
using UnityEngine;

public class CharacterAttackState : PlayerState {

    [SerializeField] private bool drawGizmo = true;
    private IHittable[] hits;
    private float startTime;

    private void OnDrawGizmos() {
        if (!drawGizmo) return;
        if (stateMachine == null || configuration == null) return;
        Gizmos.DrawWireSphere(stateMachine.PlayerController.AttackPoint.position, configuration.AttackRange);
    }

    public override void StateEnter() {
        stateMachine.PlayerController.SetIsAttacking(true);
        stateMachine.PlayerController.SetCanAttack(false);
        if (HasHit()) ApplyDamageToHitTargets();
        stateMachine.PlayerController.ChangeAnimation(Constants.PlayerAnimations.ATTACK_ANIM);
        if (stateMachine.PlayerController.Rigidbody2D.linearVelocityY > 0f) stateMachine.PlayerController.Rigidbody2D.linearVelocityY *= 0.5f;
        if (stateMachine.PlayerController.Rigidbody2D.linearVelocityX != 0f) stateMachine.PlayerController.Rigidbody2D.linearVelocityX *= 0.5f;
        startTime = Time.time;
    }

    public override void StateExit() {
        stateMachine.PlayerController.SetIsAttacking(false);
    }

    public override void StateInputs() {
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
        if (Time.time > startTime + configuration.AttackTime) {
            stateMachine.SetState((stateMachine.PlayerController.Rigidbody2D.linearVelocityX == 0f && stateMachine.PlayerController.IsGrounded) ? typeof(CharacterIdleState) : typeof(CharacterMovementState));
        }
    }

    private bool HasHit() {
        hits = Physics2D.OverlapCircleAll(stateMachine.PlayerController.AttackPoint.position, configuration.AttackRange)
            .Select(collider => collider.GetComponent<IHittable>())
            .Where(hit => hit != null)
            .ToArray();
        return hits.Length > 0;
    }

    private void ApplyDamageToHitTargets() {
        foreach (var hit in hits) {
            CharacterHitEffect hitEffect = stateMachine.PlayerController.HitEffectPool.Get();
            hitEffect.Initialize(stateMachine.PlayerController);
            hitEffect.transform.position = (hit as MonoBehaviour).transform.position;
            Vector2 hitDirection = stateMachine.PlayerController.transform.localScale.x >= 1 ? Vector2.right : Vector2.left;
            hit.Hit(new HitContext(configuration.AttackDamage, hitDirection));
        }
    }
}