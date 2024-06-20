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
    private BossMovimentation bossMov;
    private PlayerWalk playerWalk;
    private PlayerCombo playerCombo;
    //

    public Image[] lifeIcon; //tentar fazer servir pro player e pro boss, economizando espaço
    public Sprite full;
    public Sprite hollow;

    private bool round1Ended = false;
    private bool playerDiedOnce = false;
    private bool bossDiedOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<Life>();
        bossData = GameObject.Find("Enemy Test").GetComponent<Life>();
        timeCount = GameObject.Find("Main Camera").GetComponent<TimeCounter>();

        bossMov = GameObject.Find("Enemy Test").GetComponentInChildren<BossMovimentation>();//não vou conseguir parar ele com isso
        playerWalk = GameObject.Find("Player").GetComponent<PlayerWalk>();
        playerCombo = GameObject.Find("Player").GetComponent<PlayerCombo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCount.timer <= 0)
        {
            StartCoroutine(GettingRoundResults());
            //RoundCheckByTime();
        }
        else if (playerData.life == 0 || bossData.life == 0)
        {
            RoundCheck();
        }

    }

    IEnumerator GettingRoundResults()
    {
        //desativar os controles do player e do chefe aqui
        playerWalk.gameObject.SetActive(false);
        playerCombo.gameObject.SetActive(false);
        //bossMov aqui

        yield return new WaitForSeconds(4f);
        RoundCheckByTime();timeCount.timer += 61.0f;
    }

    void RoundCheckByTime()
    {


        round1Ended = true;
        //fazer uma pausa, desativar a camada superior do coração de quem perdeu o round e
        //resetar a cena(manualmente encher as vidas e retornar os personagens às suas posições iniciais sobreescrevendo o transform)
        if (playerData.life < bossData.life)
        {
            lifeIcon[0].sprite = hollow;
            //lifeIcon[0].enabled = false;
            //ícone posicionado mais a esquerda, o índice 1 será a última "vida" do jogador e
            //os índices 2 e 3 servirão da mesma forma para o chefe, sendo o índice 2 sua última vida)
            if ((playerData.life < bossData.life) && round1Ended == true)
            {
                lifeIcon[1].sprite = hollow;
                //tela de game over e restart aqui
            }
            else if (bossData.life < playerData.life)
            {
                lifeIcon[3].sprite = hollow;
            }

        }
        else if (bossData.life < playerData.life)                       //lembrando que preciso fazer isso pros 2 rounds(ganhos)
        {                                                               //daí vou precisar de uma bool nesses IF's
            lifeIcon[3].sprite = hollow;

            if ((bossData.life < playerData.life) && round1Ended == true)
            {
                lifeIcon[2].sprite = hollow;
                //tela de vitória aqui
            }
            else if (playerData.life < bossData.life)
            {
                lifeIcon[1].sprite = hollow;
            }
        }
    }


    void RoundCheck()
    {
        if (playerData.life <= 0)//&& round1Ended == false) //não tá entrando aqui na segunda vez
        {
            lifeIcon[0].sprite = hollow;
            round1Ended = true;
            //playerData.life = 200; //preciso cuidar disso aqui

            playerDiedOnce = true;
            //escurecer a tela aqui e fazer a troca de rouds 
            if (round1Ended == true)
            {
                lifeIcon[1].sprite = hollow;
                //tela de game over e restart aqui (lembrar de resetar a bool do round1
            }


        }

        if (bossData.life <= 0)//&& round1Ended == false)                       //lembrando que preciso fazer isso pros 2 rounds(ganhos)
        {
            lifeIcon[3].sprite = hollow;
            bossDiedOnce = true;

            if (round1Ended == true)
            {
                lifeIcon[2].sprite = hollow;
                //tela de vitória aqui
            }

        }

        if (bossData.life <= 0 && playerDiedOnce == true) //ROUNDS "DO MEIO"
        {
            lifeIcon[3].sprite = hollow;
        }

        if (playerData.life <= 0 && bossDiedOnce == true)
        {
            lifeIcon[1].sprite = hollow;
        }
    }

}
