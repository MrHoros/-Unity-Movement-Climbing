using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Wlasna maszyna stanow gdyz ten z Unity jest typowo pod animacje i pozwala na chilowe bycie w dwoch stanach na raz
public abstract class StateMachine : MonoBehaviour
{
    private State currentState;
    
    public void SwitchState(State newState)
    {
        if (currentState?.GetType() == newState?.GetType()) return;

        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
        Debug.Log("State switched to " + newState?.GetType());
    }

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

}
