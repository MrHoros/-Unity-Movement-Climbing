using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using UnityEngine;

//Stan po wykryciu LedgeDetector - gracz wisi w miejscu detekcji i czeka na input - przycisk pojscia do przodu (W) podnosi gracza a (S) puszcza
public class PlayerHangState : PlayerBaseState
{
    private readonly int HangHash = Animator.StringToHash("A_Hanging");
    private float CrossFadeDuration = 0.1f;
    private Vector3 ledgeForward;
    private Vector3 closestPoint;

    public PlayerHangState(PlayerStateMachine stateMachine, Vector3 ledgeForward, Vector3 closestPoint) : base(stateMachine)
    {
        this.ledgeForward = ledgeForward;
        this.closestPoint = closestPoint;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);

        stateMachine.Controller.enabled = false;
        stateMachine.transform.position = closestPoint - (stateMachine.LedgeDetector.transform.position - stateMachine.transform.position);
        stateMachine.Controller.enabled = true;

        stateMachine.Animator.CrossFadeInFixedTime(HangHash, CrossFadeDuration);
    }

    private void OnDrop()
    {
        stateMachine.ForceReceiver.Reset();
        stateMachine.Controller.Move(Vector3.zero);
        stateMachine.SwitchState(new PlayerFallState(stateMachine));
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y > 0f)
        {
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
        }
        else if (stateMachine.InputReader.MovementValue.y < 0f)
        {
            OnDrop();
        }
    }
    public override void Exit()
    {

    }
}
