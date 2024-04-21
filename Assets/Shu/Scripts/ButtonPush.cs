using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SasakiShu
/// ボタンオブジェクトの処理
/// </summary>

public class ButtonPush : MonoBehaviour
{
    [SerializeField]
    GameObject ButtonRay;
    [SerializeField,Header("表示するオブジェクト")]
    private GameObject[] ActiveObjects;
    [SerializeField,Header("非表示にするオブジェクト")]
    private GameObject[] InactiveObjects;
    [SerializeField,Header("動かし始めるオブジェクト")]
    private GameObject[] moveObjects;
    private float posY;
    [SerializeField]
    private bool isRedButton;
    private bool isPush;

    void Start()
    {
        isPush = false;
        if (!isRedButton)
            posY = this.transform.position.y;
    }

    void Update()
    {
        var ray = Physics2D.Raycast(ButtonRay.transform.position, Vector2.up, 0.001f);
        if (ray.collider != null)
        {
            //赤いボタンの時
            if (isRedButton)
            {
                if (ray.collider.CompareTag("Player") && isPush == false)
                {
                    isPush = true;
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.3f, this.transform.position.z);
                    foreach(var Object in InactiveObjects)
                    {
                        Object.SetActive(false);
                    }
                    foreach(var Object in ActiveObjects)
                    {
                        Object.SetActive(true);
                    }

                    //動かし始めるオブジェクトの処理をここに書く
                    foreach(var Object in moveObjects)
                    {
                        Object.SetActive(true);
                    }
                }
            }
            //青いボタンの時
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, posY - 0.3f, this.transform.position.z);
                foreach (var Object in InactiveObjects)
                {
                    Object.SetActive(false);
                }
                foreach (var Object in ActiveObjects)
                {
                    Debug.Log(posY);
                    Object.SetActive(true);
                }
            }
        }
        else
        {
            if (!isRedButton)
            {
                this.transform.position = new Vector3(this.transform.position.x, posY, this.transform.position.z);
                foreach (var Object in InactiveObjects)
                {
                    Object.SetActive(false);
                }
                foreach (var Object in ActiveObjects)
                {
                    Object.SetActive(true);
                }
            }
        }
    }
}