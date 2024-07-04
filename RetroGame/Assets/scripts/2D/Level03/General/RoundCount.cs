using Level2;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class RoundCount : MonoBehaviour
{
    //dados dos personagens para quando acabar o round
    [SerializeField] Life playerData;
    [SerializeField] Life bossData;
    private TimeCounter timeCount;
    float resultsTime = 4f;

    [SerializeField] Transform playerTransform;
    [SerializeField] Transform bossTransform;
    //
    private PlayerWalk playerWalk;
    private PlayerCombo playerCombo;
    //

    public Image[] lifeIcon; //tentar fazer servir pro player e pro boss, economizando espa�o
    public Sprite full;
    public Sprite hollow;

    public Image roundEnd; //transi��o de um round pro outro

    private bool isUpdateEnabled;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        playerData = GameObject.Find("Player").GetComponent<Life>();
        bossData = GameObject.Find("Enemy Test").GetComponent<Life>();
        timeCount = GameObject.Find("Main Camera").GetComponent<TimeCounter>();

        //bossMov = GameObject.Find("Enemy Test").GetComponentInChildren<BossMovimentation>();//n�o vou conseguir parar ele com isso
        playerWalk = GameObject.Find("Player").GetComponent<PlayerWalk>();
        playerCombo = GameObject.Find("Player").GetComponent<PlayerCombo>();

        Debug.Log($"Scene Reloaded - Player and boss Initialized");

        isUpdateEnabled = true;
        //FUN��O PARA CUIDAR DAS MUDAN�AS NOS CORA��ES
        //UpdateHUD();

        Debug.Log($"LifeIcon length: {lifeIcon.Length}");
        foreach(var icon in lifeIcon)
        {
            Debug.Log(icon?.name ?? "null");
        }
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

        if (isUpdateEnabled)
        {
            GameSet();

        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Time.timeScale = 1f;
        }
    }

    void GameSet()
    {
        if (playerData.fainted == true || bossData.fainted == true)//(playerData.life <= 0 || bossData.life <= 0)
        {
            GettingRoundResults();
            isUpdateEnabled = false; //para de rodar no update

            if (RoundSingleton.Instance.FinalRound() == true)
            {
                //Time.timeScale = 0.01f; //vai ficar essa c�mera lenta no final mesmo, at� a tela de game over aparecer
                Debug.Log("Ultimo round"); //Funcionando
                Time.timeScale = 0.2f; //vai ficar essa c�mera lenta no final mesmo, at� a tela de game over aparecer
                                       //playerWalk.gameObject.SetActive(false);
                                       //playerCombo.gameObject.SetActive(false);

                isUpdateEnabled = false; //para de rodar no update
                return;
            }

            Debug.Log("sa�mos do for");

        }

    }

    private void GettingRoundResults()
    {
        //float resultsTime = 4f;

        RoundCheck();

        //desativar os controles do player e do chefe aqui

        //for (float i = 0; i < resultsTime; i += Time.deltaTime)
        //{
        //    playerWalk.gameObject.SetActive(false);
        //    playerCombo.gameObject.SetActive(false);
        //    //bossMov aqui
        //}
        playerWalk.enabled = false; //o comando assim desativa SOMENTE o SCRIPT(valor que t� nessa vari�vel no momento)
        playerCombo.enabled = false;
        //playerWalk.gameObject.SetActive(false);//O PROBLEMA PROVAVELMENTE T� AQUI NESSAS
        //playerCombo.gameObject.SetActive(false);//DUAS LINHAS

        StartCoroutine(RoundTransition()); //ESSA DAQUI � A DO ROUNDCOUNT E N�O DO SINGLETON
        
    }

    private void RoundCheckByTime()
    {


        //round1Ended = true; <-isso aqui n�o funciona!

        //fazer uma pausa, desativar a camada superior do cora��o de quem perdeu o round e
        //resetar a cena(manualmente encher as vidas e retornar os personagens �s suas posi��es iniciais sobreescrevendo o transform)
        if (playerData.life < bossData.life)
        {
            lifeIcon[0].sprite = hollow;

            //lifeIcon[0].enabled = false;
            //�cone posicionado mais a esquerda, o �ndice 1 ser� a �ltima "vida" do jogador e
            //os �ndices 2 e 3 servir�o da mesma forma para o chefe, sendo o �ndice 2 sua �ltima vida)
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
        {                                                               //da� vou precisar de uma bool nesses IF's
            lifeIcon[3].sprite = hollow;


            if (RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right) < RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left))//round1Ended == true)
            {
                lifeIcon[2].sprite = hollow;
                RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Left);
                return; //por enquanto!!!
                //tela de vit�ria aqui
            }
            else
                RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Left);
            return;
        }
    }


    private void RoundCheck()
    {
        if (playerData.fainted == true)//&& round1Ended == false) //n�o t� entrando aqui na segunda vez
        {
            //escurecer a tela aqui e fazer a troca de rouds
            //StartCoroutine(RoundTransition());
            //lifeIcon[0].sprite = hollow;            
            //
            //if (RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right) > RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left)) //"se o boss tiver ganhado o primeiro round..."  - (round1Ended == true) Trocado pelo singleton
            //{
            //    //RoundSingleton.Instance.FinalRound();
            //    RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Right);
            //    //lifeIcon[1].sprite = hollow;
            //    //tela de game over e restart aqui, talvez utilizemos velocidade mais lenta, pra dar tempo das transi��es
            //}

           
            RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Right);
            playerData.fainted = false;


            //if (RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right) < 2)
            //{
            //    //playerData.life = 60f;
            //    playerData.fainted = false; //pronto pra outro round >:3
            //}

        }

        else if (bossData.fainted == true)//(bossData.life <= 0)//&& round1Ended == false)                       //lembrando que preciso fazer isso pros 2 rounds(ganhos)
        {
            //StartCoroutine(RoundTransition());
            //lifeIcon[3].sprite = hollow;

            //if (RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left) > RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right)) //"se o player tiver ganhado o primeiro round..."
            //{

            //    //RoundSingleton.Instance.FinalRound();
            //    RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Left);
            //    //lifeIcon[2].sprite = hollow;
            //    //tela de vit�ria aqui
            //}
            //else
            RoundSingleton.Instance.RecordRoundWinner(winner: RoundSingleton.Side.Left);
            bossData.fainted = false;

            //if (RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left) < 2)
            //{
            //    //bossData.life = 60f;
            //    //new WaitForEndOfFrame();
            //    bossData.fainted = false;
            //}

        }
    }

    void UpdateHUD()
    {
        //Debug.Log("Victory on boss side: " + RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right));
        //Debug.Log("Victory on Player side: " + RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left));
        if (lifeIcon[0] != null)
        {
            lifeIcon[0].sprite = RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right) > 0 ? hollow : full;
            lifeIcon[1].sprite = RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Right) > 1 ? hollow : full;
            lifeIcon[2].sprite = RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left) > 0 ? hollow : full;
            lifeIcon[3].sprite = RoundSingleton.Instance.CurrentWinsOf(RoundSingleton.Side.Left) > 1 ? hollow : full;
        }
        else Debug.LogError("lifeIcon array retornou null");
    }

    public Animator bossAnim;
    IEnumerator RoundTransition() //preciso desativar os scripts do player e do boss e retornar eles pras posi��es originais
    {                             //enquanto a transi��o est� ativa
        roundEnd.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.1f);
        roundEnd.gameObject.SetActive(false);
        

        //recarregar a cena e reativar os scripts
        RoundSingleton.Instance.ReloadScene();

        yield return new WaitForSeconds(0.1f); //pequeno delay para garantir que a cena foi carregada

        playerWalk.enabled = true;
        playerCombo.enabled = true;

        //FUN��O PARA CUIDAR DAS MUDAN�AS NOS CORA��ES
        UpdateHUD();
        //playerWalk.gameObject.SetActive(true);
        //playerCombo.gameObject.SetActive(true);
    }

}
