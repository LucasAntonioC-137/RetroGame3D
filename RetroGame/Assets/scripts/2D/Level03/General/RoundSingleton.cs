using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSingleton : MonoBehaviour
{
    public static RoundSingleton Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    //Operador Ternário => "is this condition true ? yes : no"
    public void RoundCount()
    {

    }

}
