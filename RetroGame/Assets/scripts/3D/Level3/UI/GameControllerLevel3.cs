using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Level3
{
    public class GameControllerLevel3 : MonoBehaviour
    {
        [Header("Player UI")]
        private PlayerControl player;
        public Sprite[] healthSprites; // arraste seus 7 sprites para aqui no Inspector
        public Image healthUI; // arraste o componente Image para aqui no Inspector
        public int playerScore;
        public Text scoreText;

        [Header("Boss")]
        public Sprite[] bossMood;
        public Image bossUI;
        public GameObject bossVision;
        private BossFightZone bossZone;
        private Boo booBoss;

        [Header("Bomb")]
        public Image bombOn;

        [Header("CameraStatus")]
        public Sprite[] cameraStatsSprites;
        public Image cameraUI;

        private ThirdPersonCameraController camPlayer;
        public FieldOfView fieldOV;

        // Start is called before the first frame update
        void Start()
        {
            camPlayer = GameObject.FindObjectOfType<ThirdPersonCameraController>();
            player = GameObject.FindObjectOfType<PlayerControl>();
            bossZone = GameObject.FindObjectOfType<BossFightZone>();
            booBoss = GameObject.FindObjectOfType<Boo>();
            //fieldOV = GameObject.FindObjectOfType<FieldOfView>();
            healthUI.sprite = healthSprites[(int)player.playerHealth];
            bossUI.enabled = false;
            bombOn.enabled = false;
            bossVision.SetActive(false);
            playerScore = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (!player.isDead)
            {
                playerScore = (int)player.playerScore;
                this.scoreText.text = playerScore.ToString("D6");
                PlayerHelth();
                PlayerWithBomb();
                BossMood();
                CameraStatus();
                BossSeeing();
            }
        }

        private void BossSeeing()
        {
            if (fieldOV.canSeePlayer)
            {
                bossVision.SetActive(true);
            }else
                bossVision.SetActive(false);
        }

        private void CameraStatus()
        {
            if (camPlayer.camMode)
            {
                cameraUI.sprite = cameraStatsSprites[0];
            }
            else
            {
                cameraUI.sprite = cameraStatsSprites[1]; 
            }
        }

        private void BossMood()
        {
            if (bossZone.isPlayerInside)
            {
                bossUI.enabled = true;
                if (booBoss.life > 4)
                    bossUI.sprite = bossMood[0];
                else if (booBoss.life > 2 && booBoss.life < 5)
                    bossUI.sprite = bossMood[1];
                else if (booBoss.life < 3)
                    bossUI.sprite = bossMood[2];
            }
            else
            {
                bossUI.enabled = false;
            }
        }

        private void PlayerWithBomb()
        {
            if (player.withObject)
            {
                bombOn.enabled = true;
            }
            else
            {
                bombOn.enabled = false;
            }
        }

        private void PlayerHelth()
        {
            healthUI.sprite = healthSprites[(int)player.playerHealth];
        }
    } 
}
