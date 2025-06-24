using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

//Wzbogacona maszyna stanow o wlasciwosci typowe dla gracza (dziedziczy po StateMachine na rowni z EnemyStateMachine(placeholderem))
public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }

    /*  Konfig
    FreeLookMovementSpeed - predkosc poruszania sie w domyslnym stanie (bieganie)
    RotationDamping - wrazliwosc modelu gracza na skrecanie kiedys jest domyslnym stanie (bieganie)
    JumpRotationDamping - wrazliwosc modelu gracza na skrecanie podczas skoku
    JumpForce - jak wysoko gracz skacze
    */
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float JumpRotationDamping { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    
    public Transform MainCameraTransform { get; private set; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }

}
