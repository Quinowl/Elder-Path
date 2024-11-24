using System.Linq;
using UnityEngine;

public class CharacterAttackState : PlayerState {

    [SerializeField] private bool drawGizmo = true;

    private EnemyDamageable[] hits;
    private float startTime;

    private void OnDrawGizmos() {
        if (!drawGizmo) return;
        if (stateMachine == null || configuration == null) return;
        Gizmos.DrawWireSphere(stateMachine.PlayerController.AttackPoint.position, configuration.AttackRange);
    }

    public override void StateEnter() {
        stateMachine.PlayerController.SetIsAttacking(true);
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX = 0f;
        if (HasHit()) ApplyDamageToHitTargets();
        stateMachine.PlayerController.ChangeAnimation(Constants.PLAYER_ATTACK_ANIM);
        stateMachine.PlayerController.Rigidbody2D.linearVelocity = Vector2.zero;
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
            stateMachine.SetState(stateMachine.PlayerController.Rigidbody2D.linearVelocityX == 0f ? typeof(CharacterIdleState) : typeof(CharacterMovementState));
        }
    }

    private bool HasHit() {
        hits = Physics2D.OverlapCircleAll(stateMachine.PlayerController.AttackPoint.position, configuration.AttackRange)
            .Select(collider => collider.GetComponent<EnemyDamageable>())
            .Where(enemy => enemy != null)
            .ToArray();
        return hits.Length > 0;
    }

    private void ApplyDamageToHitTargets() {
        foreach (var hit in hits) {
            hit.TakeDamage(configuration.AttackDamage);
        }
    }
}