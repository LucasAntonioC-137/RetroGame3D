using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        if(btn != null)
        {
            btn.onClick.AddListener(ChangeScene);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
