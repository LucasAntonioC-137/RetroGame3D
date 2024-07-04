using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void Start()
    {
        Debug.Log($"Start - Current wins - Left: {RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left)}," +
                                        $" Right: {RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right)}");
    }

    void OnDestroy()
    {
        Debug.Log("Singleton OnDestroy: " + gameObject.name);
    }

    private const int WINS_REQUIRED = 2;
    int _leftWins, _rightWins;

    //void Awake() => Reset(); fazer uma fun��o de reset do n�vel, temos a antiga do n�vel 2 "ResetLevel"
    void OnEnable() => Instance = this;
//    void OnDisable() => Instance = null; PROBLEMA ERA NESSE CORNO que tava nulificando o singleton
    public void Reset() { _leftWins = _rightWins = 0; }

    public void RecordRoundWinner(Side winner)
    {
        setWins(winner, getWins(winner) + 1);
        Debug.Log($"Recorded win for {winner}. Current wins of: Left: {_leftWins}, Right: {_rightWins}");
    }

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
    //Operador Tern�rio => "is this condition true ? yes : no"
    
    public void ReloadScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        //StartCoroutine(RoundTransition()); ATIVA ISSO AQUI MAIS N�O MEU AMIGO PFV

        SceneManager.LoadScene(sceneName);
    }

    public Image roundEnd; //transi��o de um round pro outro
    IEnumerator RoundTransition() //preciso desativar os scripts do player e do boss e retornar eles pras posi��es originais
    {                             //enquanto a transi��o est� ativa
        roundEnd.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(2.19f);

        roundEnd.gameObject.SetActive(false);
    }
}
