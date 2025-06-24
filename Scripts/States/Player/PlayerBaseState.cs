using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa dodajaca uniwersalne wlasciwosci stanow typowo pod gracza ( dziedziczy po State na rowni z EnemyBaseState (placeholderem))
public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }
   protected void ReturnToLocomotion()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

}
