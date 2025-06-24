using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinDisplayer : MonoBehaviour
{
    [SerializeField] CoinCounter CoinCounter;
    [SerializeField] GameManager GameManager;
    TextMeshProUGUI _textMesh;

    void Start()
    {
        if (CoinCounter == null) return;
        _textMesh = gameObject.GetComponent<TextMeshProUGUI>();

        CoinCounter.CoinPickedUpEvent += Display;

        Display(0);
    }
    public void Display(int NumberToDisplay)
    {
        if (GameManager == null ||_textMesh == null) return;
        _textMesh.text = NumberToDisplay.ToString() + " / " + GameManager.CollectedCoinsGoal.ToString();
    }
}
