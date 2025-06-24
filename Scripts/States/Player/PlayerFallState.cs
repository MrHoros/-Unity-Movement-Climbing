using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

//State ktory odpala sie podczas opadania gracza, utrzymuje wczesniejsze momentum
public class PlayerFallState : PlayerBaseState
{
     private readonly int FallHash = Animator.StringToHash("A_Fall");
    private const float CrossFadeDuration = 0.1f;
    private Vector3 momentum;
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.LedgeDetector.LedgeDetectEvent += OnLedgeDetected;
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0;

        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
    }
    private void OnLedgeDetected(Vector3 ledgeForward, Vector3 closestPoint)
    {
        stateMachine.SwitchState(new PlayerHangState(stateMachine, ledgeForward, closestPoint));
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if(stateMachine.Controller.isGrounded)
        {
            ReturnToLocomotion();
        }

    }
    public override void Exit()
    {

    }


}
