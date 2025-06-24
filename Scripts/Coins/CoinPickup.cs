using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] CoinCounter CoinCounter;
   private void OnTriggerEnter(Collider other) {
        if (CoinCounter == null) return;
        CoinCounter.AddCoin();
        Destroy(gameObject);
    }
}
