using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSingleton : MonoBehaviour
{
    public static RoundSingleton Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private const int WINS_REQUIRED = 2;
    int _leftWins, _rightWins;

    //void Awake() => Reset(); fazer uma função de reset do nível, temos a antiga do nível 2 "ResetLevel"
    void OnEnable() => Instance = this;
    void OnDisable() => Instance = null;
    void OnDestroy() => OnDisable();
    public void Reset() { _leftWins = _rightWins = 0; }

    public void RecordRoundWinner(Side winner) 
        => setWins(winner, getWins(winner) + 1);

    public int CurrentWinsOf(Side side) => getWins(side);

    public int totalRounds => _leftWins + _rightWins;
    
    public bool FinalRound() 
        => _leftWins == WINS_REQUIRED || _rightWins == WINS_REQUIRED;

    public Side finalWinner => _leftWins > _rightWins ? Side.Left : Side.Right;

    private int getWins(Side side)
        => side == Side.Right ? _rightWins : _leftWins;

    private void setWins(Side side, int value)
    {
        if (side == Side.Right) _rightWins = value;
        else _leftWins = value;
    }

    public enum Side
    {
        Left = 0,
        Right = 1
    }
    //Operador Ternário => "is this condition true ? yes : no"
  
}
