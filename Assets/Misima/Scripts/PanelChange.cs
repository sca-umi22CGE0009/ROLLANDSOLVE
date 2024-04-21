using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelChange : MonoBehaviour
{
    public GameObject Panel_Operation;
    public GameObject Panel_Claer1;
    public GameObject Panel_Claer2;
    public GameObject Panel_Claer3;

    // Start is called before the first frame update
    void Start()
    {
        Panel_Operation.SetActive(true);
        Panel_Claer1.SetActive(false);
        Panel_Claer2.SetActive(false);
        Panel_Claer3.SetActive(false);
        Debug.Log("�ꖇ�ڂ̃p�l���̕\��");
    }

    public void CloseView()
    {
        Panel_Operation.SetActive(true);
        Panel_Claer1.SetActive(false);
    }

    public void NextView()
    {
        Panel_Operation.SetActive(false);
        Panel_Claer1.SetActive(true);
        Panel_Claer2.SetActive(false);
        Panel_Claer3.SetActive(false);
        Debug.Log("�񖇖ڂ̃p�l���̕\��");
    }

    public void NextView1()
    {
        Panel_Operation.SetActive(false);
        Panel_Claer1.SetActive(false);
        Panel_Claer2.SetActive(true);
        Panel_Claer3.SetActive(false);
        Debug.Log("�O���ڂ̃p�l���̕\��");
    }

    public void NextView2()
    {
        Panel_Operation.SetActive(false);
        Panel_Claer1.SetActive(false);
        Panel_Claer2.SetActive(false);
        Panel_Claer3.SetActive(true);
        Debug.Log("�l���ڂ̃p�l���̕\��");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Panel_Operation.activeSelf)
            {
                NextView();
            }
            else if (Panel_Claer1.activeSelf)
            {
                NextView1();
            }
            else if (Panel_Claer2.activeSelf)
            {
                NextView2();
            }
            // You can add more conditions if needed for additional panels
        }
    }
}
