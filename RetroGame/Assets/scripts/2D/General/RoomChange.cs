using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomChange : MonoBehaviour
{
    public Transform target;
    public Vector3 playerChange;
    private BasicCameraFollow cam; //script da câmera
    public bool needText;
    public string placeName; //nome do lugar/sala
    public GameObject text;
    public Text placeText;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<BasicCameraFollow>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.transform.position += playerChange;
            if(needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }
    
    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(playerChange, playerChange);
        
    }
}
