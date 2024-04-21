using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// u.sasak
/// </summary>
public class HiddenMedal : MonoBehaviour
{
    float posY;
    bool touchCheck = false;
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        //非表示にする
        if (touchCheck)
        {
            gameObject.SetActive(false);
        }
    }
    //プレイヤーに触ってる
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerController.playerstate == PlayerController.PlayerState.Human)
                touchCheck = true;
        }
    }
}
