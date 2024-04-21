using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SasakiShu
/// �{�^���I�u�W�F�N�g�̏���
/// </summary>

public class ButtonPush : MonoBehaviour
{
    [SerializeField]
    GameObject ButtonRay;
    [SerializeField,Header("�\������I�u�W�F�N�g")]
    private GameObject[] ActiveObjects;
    [SerializeField,Header("��\���ɂ���I�u�W�F�N�g")]
    private GameObject[] InactiveObjects;
    [SerializeField,Header("�������n�߂�I�u�W�F�N�g")]
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
            //�Ԃ��{�^���̎�
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

                    //�������n�߂�I�u�W�F�N�g�̏����������ɏ���
                    foreach(var Object in moveObjects)
                    {
                        Object.SetActive(true);
                    }
                }
            }
            //���{�^���̎�
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