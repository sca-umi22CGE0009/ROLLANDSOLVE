using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Example : MonoBehaviour
{
    public Button Stage1Button,Stage2Button,Stage3Button;

    void stage()
    {
        Stage1Button.onClick.AddListener(TaskOnClick);
        Stage2Button.onClick.AddListener(delegate{TaskWithParameter("Hello");});
        Stage3Button.onClick.AddListener(()=> ButtonClicked(42));
        Stage3Button.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            SceneManager.LoadScene("Stage1");
        }
    }

    public void TaskWithParameter(string message)
    {
        Debug.Log(message);
    }

    public void ButtonClicked(int buttonNo)
    {
        Debug.Log("ボタンがクリックされた = "+ buttonNo);
    }
}

