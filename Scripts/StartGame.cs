using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//dodatkowy modul Loading Screen Studio
using Michsky.LSS;

//Obsluga przycisku Start przy uzyciu pluginy Loading Screen Studio
public class StartGame : MonoBehaviour
{
    [SerializeField] LSS_Manager LSS_Manager;
    [SerializeField] string SceneToStartName;

    private bool _debounce;
    public void LoadStartScene()
    {
        if (_debounce) return;
        if (SceneToStartName == "") return;

        LSS_Manager.LoadScene(SceneToStartName);
    }
}
