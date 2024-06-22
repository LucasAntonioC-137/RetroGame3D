using Level2;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoundCount : MonoBehaviour
{
    //dados dos personagens para quando acabar o round
    [SerializeField] Life playerData;
    [SerializeField] Life bossData;
    private TimeCounter timeCount;

    //
    private PlayerWalk playerWalk;
    private PlayerCombo playerCombo;
    //

    public Image[] lifeIcon; //tentar fazer servir pro player e pro boss, economizando espaço
    public Sprite full;
    public Sprite hollow;

    private bool round1Ended = false;
    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<Life>();
        bossData = GameObject.Find("Enemy Test").GetComponent<Life>();
        timeCount = GameObject.Find("Main Camera").GetComponent<TimeCounter>();

        //bossMov = GameObject.Find("Enemy Test").GetComponentInChildren<BossMovimentation>();//não vou conseguir parar ele com isso
        playerWalk = GameObject.Find("Player").GetComponent<PlayerWalk>();
        playerCombo = GameObject.Find("Player").GetComponent<PlayerCombo>();

    }

    // Update is called once per frame
    void Update()
    {
        if (timeCount.timer <= 0)
        {
            float resultsTime = 4f;
            RoundCheckByTime();

            for (float i = 0; i < resultsTime; i += Time.deltaTime)
            {
                playerWalk.gameObject.SetActive(false);
                playerCombo.gameObject.SetActive(false);
            }
            playerWalk.gameObject.SetActive(true);
            playerCombo.gameObject.SetActive(true);

            timeCount = GameObject.Find("Main Camera").GetComponent<TimeCounter>();
            timeCount.timer += 61f;
        }

        if(playerData.life <= 0 || bossData.life <= 0)
        {
            GettingRoundResults();
            if (RoundSingleton.Instance.FinalRound())
            {
                playerWalk.gameObject.SetActive(false);
                playerCombo.gameObject.SetActive(false);
                
                //do boss aqui
            }
            return;
        }
    }

    private void GettingRoundResults()
    {
        float resultsTime = 4f;

        RoundCheck();

        //desativar os controles do player e do chefe aqui

        for (float i = 0; i < resultsTime; i += Time.deltaTime)
        {
            playerWalk.gameObject.SetActive(false);
            playerCombo.gameObject.SetActive(false);
        }
        playerWalk.gameObject.SetActive(true);
        playerCombo.gameObject.SetActive(true);
        
        timeCount = GameObject.Find("Main Camera").GetComponent<TimeCounter>();
        timeCount.timer += 61f;
        //bossMov aqui
    }

    private void RoundCheckByTime()
    {


        //round1Ended = true; <-isso aqui não funciona!

        //fazer uma pausa, desativar a camada superior do coração de quem perdeu o round e
        //resetar a cena(manualmente encher as vidas e retornar os personagens às suas posições iniciais sobreescrevendo o transform)
        if (playerData.life < bossData.life)
        {
            lifeIcon[0].sprite = hollow;
            
            //lifeIcon[0].enabled = false;
            //ícone posicionado mais a esquerda, o índice 1 será a última "vida" do jogador e
            //os índices 2 e 3 servirão da mesma forma para o chefe, sendo o índice 2 sua última vida)
            if (RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right) > RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left))// round1Ended == true)
            {
                lifeIcon[1].sprite = hollow;
                RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Right);
                return; //por enquanto!!!
                //tela de game over e restart aqui - PLAYER PERDEU
            }
            else
                RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Right);
                return;
        }
        else if (bossData.life < playerData.life)                       //lembrando que preciso fazer isso pros 2 rounds(ganhos)
        {                                                               //daí vou precisar de uma bool nesses IF's
            lifeIcon[3].sprite = hollow;
            

            if (RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right) < RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left))//round1Ended == true)
            {
                lifeIcon[2].sprite = hollow;
                RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Left);
                return; //por enquanto!!!
                //tela de vitória aqui
            }
            else
                RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Left);
                return;
        }
    }


    private void RoundCheck()
    {
        if (playerData.fainted == true)//&& round1Ended == false) //não tá entrando aqui na segunda vez
        {
            lifeIcon[0].sprite = hollow;
            
            
            
            
            //escurecer a tela aqui e fazer a troca de rouds 
            if(RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right) > RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left)) //"se o boss tiver ganhado o primeiro round..."  - (round1Ended == true) Trocado pelo singleton
            {
                RoundSingleton.Instance.FinalRound();
                lifeIcon[1].sprite = hollow;
                //tela de game over e restart aqui (lembrar de resetar a bool do round1
            } 
            
            else
            { RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Right); }

            if(RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right) < 2) 
            {
                playerData.life = 30f;
                playerData.fainted = false; //pronto pra outro round >:3
            }
            
        }

        else if (bossData.fainted == true)//(bossData.life <= 0)//&& round1Ended == false)                       //lembrando que preciso fazer isso pros 2 rounds(ganhos)
        {
            lifeIcon[3].sprite = hollow;

            

            if (RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left) > RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left)) //"se o player tiver ganhado o primeiro round..."
            {
                RoundSingleton.Instance.FinalRound();
                lifeIcon[2].sprite = hollow;
                //tela de vitória aqui
            } 
            
            else
            { RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Left); }

            if(RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left) < 2)
            {
                bossData.life = 30f;
                bossData.fainted = false;
            }
                       
        }
    }

}
