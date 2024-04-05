using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3
{
    public class PortalSpawn : MonoBehaviour
    {
        public GameObject portalEffect;
        public GameObject tvBlock;
        public float moveAmount = 1f;
        public bool bossIsDead = false;
        private bool activated = false;
        private PlayerControl playerScript;
        private Boo booLife;
        public CinemachineVirtualCamera[] virtualCameras;
        public AudioSource[] portalSounds;
        private BossFightZone bossZone;
        
        // Start is called before the first frame update
        void Start()
        {
            bossZone = GameObject.FindObjectOfType<BossFightZone>();
            playerScript = GameObject.FindObjectOfType<PlayerControl>();
            booLife = GameObject.FindObjectOfType<Boo>();
            tvBlock.SetActive(false);
            portalEffect.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!booLife.bossLive && !activated)
            {
                activated = true;
                StartCoroutine(SpawnPortalEffect());
            }
        }
        IEnumerator SpawnPortalEffect()
        {
            playerScript.cameraInCutScene = true;
            virtualCameras[0].Priority = 0;
            virtualCameras[1].Priority = 1;
            yield return new WaitForSeconds(0.5f);
            tvBlock.SetActive(true);
            portalEffect.SetActive(true);
            portalSounds[0].Play();
            gameObject.GetComponent<BoxCollider>().enabled= true;
            yield return new WaitForSeconds(1f);
            portalSounds[1].Play();
            yield return new WaitForSeconds(1.5f);
            virtualCameras[0].Priority = 1;
            virtualCameras[1].Priority = 0;
            playerScript.cameraInCutScene = false;
            bossZone.gameTheme.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                Debug.Log("TELA FINAL");
            }
        }
    } 
}
