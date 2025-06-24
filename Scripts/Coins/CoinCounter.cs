using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Klasa przypinana do gracza zliczajaca zebrane coiny
public class CoinCounter : MonoBehaviour
{
    public event Action<int> CoinPickedUpEvent;
    private int _coinsCollected;

    public void AddCoin()
    {
        _coinsCollected += 1;
        Debug.Log("Coin picked up! +1");

        CoinPickedUpEvent.Invoke(_coinsCollected);
    }
}
