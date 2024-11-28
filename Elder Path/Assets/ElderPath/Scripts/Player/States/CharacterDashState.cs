using System.Collections;
using UnityEngine;

public class CharacterDashState : PlayerState {

    private float startTime;
    private Vector3 lastTrailPosition;
    private Coroutine trailGenerationCoroutine;

    public override void StateEnter() {
        stateMachine.PlayerController.SetCanDash(false);
        startTime = Time.time;
        stateMachine.PlayerController.Rigidbody2D.linearVelocityY *= 0.3f;
        stateMachine.PlayerController.TryMoveX(configuration.DashForce * stateMachine.PlayerController.transform.localScale.x);
        stateMachine.PlayerController.SetIsDashing(true);
        trailGenerationCoroutine = StartCoroutine(StartTrailGeneration());
    }

    public override void StateExit() {
        //TODO: Change these magic numbers
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX *= 0.25f;
        stateMachine.PlayerController.SetIsDashing(false);
        StopCoroutine(trailGenerationCoroutine);
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.AttackInput && stateMachine.PlayerController.CanAttack) stateMachine.SetState(typeof(CharacterAttackState));
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
        if (Time.time > startTime + configuration.DashTime) stateMachine.SetState(typeof(CharacterMovementState));
    }

    private IEnumerator StartTrailGeneration() {
        lastTrailPosition = stateMachine.PlayerController.transform.position;
        while (true) {
            if (Vector3.Distance(stateMachine.PlayerController.transform.position, lastTrailPosition) >= configuration.TrailSpawnDistance) {
                SpawnTrail();
                lastTrailPosition = stateMachine.PlayerController.transform.position;
            }
            yield return null;
        }
    }

    private void SpawnTrail() {
        PlayerTrail trail = stateMachine.PlayerController.TrailPool.Get();
        trail.Initialize(stateMachine.PlayerController);
    }
}