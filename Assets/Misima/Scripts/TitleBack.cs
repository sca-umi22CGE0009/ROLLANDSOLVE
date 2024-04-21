using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleBack : MonoBehaviour
{ 
    public string sceneToLoad; // 切り替えたいシーンの名前

    //private bool inputReceived = false;

    void Update()
    {
    /*if (inputReceived)
        {
            return;
            Debug.Log("通った");
        }*/
    Time.timeScale = 1.0f;
    if (Input.GetKeyUp(KeyCode.Return))
        {
            //inputReceived = true;
            Debug.Log("押された");
            LoadScene();

        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("各シーンへ");
    }
}
