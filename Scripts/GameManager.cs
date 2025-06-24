using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.LSS;

//Klasa sprawdzajaca osiagniecie celu przez gracza i przerzucajaca go w odpowiednia scene
//Odpowiedzialna za przenoszenie gracza do sceny konczacej gre po osiegnieciu wymagajacej liczby coinow 
public class GameManager : MonoBehaviour
{
    [SerializeField] LSS_Manager LSS_Manager;
    [SerializeField] string SceneToLoadOnGoalReached;
    [field: SerializeField]public int CollectedCoinsGoal {get; private set;}

    [SerializeField] CoinCounter CoinCounter;
    private void OnEnable() {
        if (CoinCounter == null) return;

        CoinCounter.CoinPickedUpEvent += CheckForEndGameGoal;
    }
    private void CheckForEndGameGoal(int coinsCollected)
    {
        if (coinsCollected >= CollectedCoinsGoal)
        {
            Debug.Log("Congrats! All coins collected!");

            if (LSS_Manager == null || SceneToLoadOnGoalReached == "") return;
            LSS_Manager.LoadScene(SceneToLoadOnGoalReached);
        }
    }

    private void OnDisable() {
        if (CoinCounter == null) return;
            
        CoinCounter.CoinPickedUpEvent -= CheckForEndGameGoal;
    }
}
