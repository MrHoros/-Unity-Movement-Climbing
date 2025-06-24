using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private readonly int PullUpHash = Animator.StringToHash("A_PullUp");
    private readonly Vector3 Offset = new Vector3(-0.17761f, 2.8995f, 0.74057f);

    private const float CrossFadeDuration = 0.1f;

    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PullUpHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) return;

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, false));

        stateMachine.Controller.enabled = false;
        stateMachine.transform.Translate(Offset, Space.Self);
        stateMachine.Controller.enabled = true;
    }

    public override void Exit()
    {
        stateMachine.Controller.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }
}
