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
        public PlayerControl player;
        public Sprite[] healthSprites; // arraste seus 7 sprites para aqui no Inspector
        public Image healthUI; // arraste o componente Image para aqui no Inspector
        public float playerScore;
        public Text scoreText;

        [Header("Boss")]
        public Sprite[] bossMood;
        public Image bossUI;
        public GameObject bossVision;

        [Header("CameraStatus")]
        public Image bombOn;

        [Header("CameraStatus")]
        public Sprite[] cameraStatsSprites;
        public Image cameraUI;

        // Start is called before the first frame update
        void Start()
        {
            healthUI.sprite = healthSprites[(int)player.playerHealth];
            bombOn.enabled = false;
            playerScore = player.playerScore = 0;
        }

        // Update is called once per frame
        void Update()
        {
            this.scoreText.text = PlayerStats.instance.score.ToString("D6");
            PlayerHelth();
            PlayerWithBomb();
            BossMood();
            CameraStatus();
        }

        private void CameraStatus()
        {
            
        }

        private void BossMood()
        {
            
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
