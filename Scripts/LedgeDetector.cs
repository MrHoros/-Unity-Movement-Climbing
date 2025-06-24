using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LedgeDetector jest to cube umieszczony w prefabie gracza na odpowiedniej wysokosci, ktory wykrywa kolizje z obiektami na warstwie kolizji "LedgeDetector"
//poprzez event wysyla kierunek w ktorym jest obrocony i najblizszy punkt kolizji
public class LedgeDetector : MonoBehaviour
{
    public event Action<Vector3, Vector3> LedgeDetectEvent;
    private void OnTriggerEnter(Collider other) 
    {
        LedgeDetectEvent?.Invoke(other.transform.forward, other.ClosestPointOnBounds(transform.position));
    }
}
