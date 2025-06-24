using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("A_Jump");
    private const float CrossFadeDuration = 0.1f;
    private Vector3 momentum;
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);

        stateMachine.LedgeDetector.LedgeDetectEvent += OnLedgeDetected;
    }

    //Funkcja pozwala na lekkie sterowanie graczem w powietrzu podczas skoku
    private void LerpMomentumToInputMovement(float deltaTime)
    {
        Vector3 cameraRight = stateMachine.MainCameraTransform.right;
        Vector3 cameraForward = stateMachine.MainCameraTransform.forward;

        cameraRight.y = 0;
        cameraForward.y = 0;

        cameraRight.Normalize();
        cameraForward.Normalize();        

        Vector3 movementDirection = cameraForward * stateMachine.InputReader.MovementValue.y +
            cameraRight * stateMachine.InputReader.MovementValue.x;

        momentum = Vector3.Lerp(momentum, movementDirection * stateMachine.FreeLookMovementSpeed, deltaTime);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue != Vector2.zero)
        {
            LerpMomentumToInputMovement(deltaTime);
        }

        Move(momentum, deltaTime);

        if (momentum != Vector3.zero)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(momentum),
            deltaTime * stateMachine.JumpRotationDamping);
        }

        if (stateMachine.Controller.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
            return;
        }

    }

    private void OnLedgeDetected(Vector3 ledgeForward, Vector3 closestPoint)
    {
        stateMachine.SwitchState(new PlayerHangState(stateMachine, ledgeForward, closestPoint));
    }

    public override void Exit()
    {
        stateMachine.LedgeDetector.LedgeDetectEvent -= OnLedgeDetected;
    }
}
