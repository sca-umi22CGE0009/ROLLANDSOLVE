using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SasakiShu
/// プレイヤー当たり判定(右)
/// </summary>
public class HitRightCheck : MonoBehaviour
{
    private PlayerController playercontroller;
    private HumanMotion humanMotion;
    private CapsuleMotion capsuleMotion;
    // Start is called before the first frame update
    void Start()
    {
        playercontroller = FindObjectOfType<PlayerController>();
        humanMotion = FindObjectOfType<HumanMotion>();
        capsuleMotion = FindObjectOfType<CapsuleMotion>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerStop()
    {
        playercontroller.speed = 0.0f;
        if (!playercontroller.isJump)
        {
            humanMotion.HumanStop();
            capsuleMotion.CapsuleStop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            Debug.Log("stageにぶつかった");
            playercontroller.isHitRight = true;
            PlayerStop();
        }

        if (playercontroller.playerstate == PlayerController.PlayerState.Human)
        {
            if (collision.gameObject.tag == "Wall")
            {
                playercontroller.isHitRight = true;
                PlayerStop();
            }

            if (collision.gameObject.tag == "WoodBox")
            {
                playercontroller.hitWoodBoxRight = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            playercontroller.isHitRight = false;
        }

        if (playercontroller.playerstate == PlayerController.PlayerState.Human)
        {
            if (collision.gameObject.tag == "Wall")
            {
                playercontroller.isHitRight = false;
            }

            if (collision.gameObject.tag == "WoodBox")
            {
                playercontroller.hitWoodBoxRight = false;
                playercontroller.pushstate = PlayerController.PushState.NotPushing;
            }
        }
    }
}
