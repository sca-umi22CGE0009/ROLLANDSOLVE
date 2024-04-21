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
        //Debug.DrawRay(origin, dir,UnityEngine.Color.red, 100f, true);
        //Vector2 pos = new Vector2(0, posY) * Time.deltaTime;
        //transform.Translate(pos);
        //if (posY < 0)
        //{
        //    posY += 0.1f;
        //}
        //if (posY >= 1)
        //{
        //    posY -= 0.1f;
        //}
        if (touchCheck)
        {
            gameObject.SetActive(false);
        }
    }
    //ÉvÉåÉCÉÑÅ[Ç…êGÇ¡ÇƒÇÈ
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerController.playerstate == PlayerController.PlayerState.Human)
                touchCheck = true;
        }
    }
}
