using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardInput : MonoBehaviour
{
    public Button AButton;
    public Button DButton;

/*    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventSystem.current.SetSelectedGameObject(AButton.gameObject);
            
            Debug.Log(AButton.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            EventSystem.current.SetSelectedGameObject(DButton.gameObject);
            
            Debug.Log(DButton.gameObject);
        }
    }*/

    public void ABCButton(int num)
    {
        switch (num)
        {
            case 1:
                Debug.Log("Aのボタンが押されたよ");
                break;
            case 2:
                Debug.Log("Bのボタンが押されたよ");
                break;
            case 3:
                Debug.Log("Cのボタンが押されたよ");
                break;
        }
    }
}