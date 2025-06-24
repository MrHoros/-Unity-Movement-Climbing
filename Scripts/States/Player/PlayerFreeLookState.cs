using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//Stan domyslny gracza, podczas poruszania sie po ziemi
public class PlayerFreeLookState : PlayerBaseState
{
    private bool shouldFade;

    //Zhashowane nazwy jakie ustawione zostaly w Animatorze w unity
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    
    //Konfig szybkosci zmiany animacji
    private const float AnimatorDampTimeToRun = 0.02f;
    private const float AnimatorDampTimeToIdle = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
    {
        this.shouldFade = shouldFade;
    }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;

        if (shouldFade)
        {
            stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
        }
        else
        {
            stateMachine.Animator.Play(FreeLookBlendTreeHash); //Zmiana animacji bez fade'a - potrzebne do przejscia ze stanu climbup do freelook, zeby kamera nie 'przeskakiwala'
        }

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        if (movement == Vector3.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTimeToIdle, deltaTime);
            return;
        };

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTimeToRun, deltaTime);
        FaceMovementDirection(movement, deltaTime);

    }
    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;   
    }

    private Vector3 CalculateMovement()
    {
        Vector3 cameraRight = stateMachine.MainCameraTransform.right;
        Vector3 cameraForward = stateMachine.MainCameraTransform.forward;

        cameraRight.y = 0;
        cameraForward.y = 0;

        cameraRight.Normalize();
        cameraForward.Normalize();        

        return cameraForward * stateMachine.InputReader.MovementValue.y +
            cameraRight * stateMachine.InputReader.MovementValue.x;
    
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
        stateMachine.transform.rotation,
        Quaternion.LookRotation(movement),
        deltaTime * stateMachine.RotationDamping);
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

}

