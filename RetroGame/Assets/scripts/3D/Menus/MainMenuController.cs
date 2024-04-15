using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using System;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    //BackGround Items
    [Header("BackGround Itens")]
    public Button[] changeBackGroundVideoButtons;
    public VideoPlayer backGroundVideoPlayer; // Drag your VideoPlayer here in inspector
    public VideoClip[] backGroundVideoClips; // Assign your VideoClip here in inspector
    public RawImage backgroundImage;
    public GameObject[] gameImages;
    public AudioSource selectExample;
    public AudioSource selectOption;

    [Header("Menu Options Screens")]
    public GameObject[] mainMenuOptionsScreens;

    [Header("Controls")]
    public GameObject[] controlScreens;

    // Start is called before the first frame update
    void Start()
    {
        //backGroundVideoPlayer.clip = backGroundVideoClips[0];
        //backGroundVideoPlayer.Play();
        //Listers
        backgroundImage.enabled = false;
        InstantiateBackGroundListeners();
        gameImages[0].SetActive(true);
        //InstantiatChangeSizingOfButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectSound()
    {
        selectOption.Play();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Nivel 1 - 3D");
    }

    //Menu Options
    public void Controls()
    {
        mainMenuOptionsScreens[1].SetActive(true);
        mainMenuOptionsScreens[0].SetActive(false);
        controlScreens[0].SetActive(true);
    }

    //Controls
    public void ControlsReturnPreviousPage()
    {
        Debug.Log("Voltar");
        if (controlScreens[0].activeSelf)
        {
            controlScreens[0].SetActive(false);
            mainMenuOptionsScreens[0].SetActive(true);
            mainMenuOptionsScreens[1].SetActive(false);
        }else if(controlScreens[1].activeSelf)
        {
            controlScreens[1].SetActive(false);
            controlScreens[0].SetActive(true);
        }else
        {
            controlScreens[2].SetActive(false);
            controlScreens[1].SetActive(true);
        }
    }

    public void ControlsGoNextPage()
    {
        Debug.Log("Proximo");
        if (controlScreens[0].activeSelf)
        {
            controlScreens[0].SetActive(false);
            controlScreens[1].SetActive(true);
        }
        else if (controlScreens[1].activeSelf)
        {
            controlScreens[1].SetActive(false);
            controlScreens[2].SetActive(true);
        }
    }


    void InstantiateBackGroundListeners()
    {
        for (int i = 0; i < changeBackGroundVideoButtons.Length; i++)
        {
            int index = i; // To avoid the closure problem in C#
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => { DisableImage(); ChangeBackGroundVideo(backGroundVideoClips[index], gameImages[index]); });

            EventTrigger trigger = changeBackGroundVideoButtons[i].gameObject.AddComponent<EventTrigger>();
            trigger.triggers.Add(entry);
        }
    }

    void DisableImage()
    {
        foreach(GameObject objetos in gameImages)
        {
            objetos.SetActive(false);
        }
    }
    void ChangeBackGroundVideo(VideoClip clip, GameObject image)
    {
        image.SetActive(true);
        backGroundVideoPlayer.Stop();
        backGroundVideoPlayer.clip = clip;
        backGroundVideoPlayer.isLooping = true;
        backGroundVideoPlayer.Play();
        selectExample.Play();
        backgroundImage.enabled = false;
        //backGroundVideoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        backgroundImage.enabled = true;
    }

}
