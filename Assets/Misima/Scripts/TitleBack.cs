using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleBack : MonoBehaviour
{ 
    public string sceneToLoad; // �؂�ւ������V�[���̖��O

    //private bool inputReceived = false;

    void Update()
    {
    /*if (inputReceived)
        {
            return;
            Debug.Log("�ʂ���");
        }*/
    Time.timeScale = 1.0f;
    if (Input.GetKeyUp(KeyCode.Return))
        {
            //inputReceived = true;
            Debug.Log("�����ꂽ");
            LoadScene();

        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("�e�V�[����");
    }
}
