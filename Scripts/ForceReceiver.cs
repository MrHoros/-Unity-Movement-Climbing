using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Osobna klasa odpowiadajaca za fizyke gracza (grawitacje i kontrole nad rigidbody)
public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    private float verticalVelocity;

    //Expression-Bodied Members (Składowe z wyrażeniem w treści)
    public Vector3 Movement => Vector3.up * verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }

    public void AddForce(Vector3 force)
    {
  
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }

    internal void Reset()
    {
        verticalVelocity = 0f;
    }
}