using System.Linq;
using UnityEngine;

public class CharacterAttackState : PlayerState {

    private EnemyDamageable[] hits;

    public override void StateEnter() {
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX = 0f;
        if(HasHit()) ApplyDamageToHitTargets();
        // stateMachine.PlayerController.Animator.SetTrigger();
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
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