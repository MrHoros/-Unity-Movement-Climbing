using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Klasa odczytujaca input z nowego input systemu od Unity (Controls generowane jest automatycznie przez unity, a InputReader podpina sie pod jego interfejs)
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public Vector2 MovementValue {get; private set; } = new Vector2();

    private void Start()
    {
        controls = new Controls();
        controls.Player.AddCallbacks(this);
        controls.Enable();
    }

    private void OnDestroy() 
    {
        controls.Disable();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        DodgeEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        JumpEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }
}
