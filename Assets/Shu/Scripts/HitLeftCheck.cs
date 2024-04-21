using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SasakiShu
/// �v���C���[�����蔻��(��)
/// </summary>
public class HitLeftCheck : MonoBehaviour
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
        Debug.Log("hitCollider���Ԃ�����");
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
            playercontroller.isHitLeft = true;
            PlayerStop();
        }

        if (playercontroller.playerstate == PlayerController.PlayerState.Human)
        {
            if (collision.gameObject.tag == "Wall")
            {
                playercontroller.isHitLeft = true;
                PlayerStop();
            }

            if (collision.gameObject.tag == "WoodBox")
            {
                playercontroller.hitWoodBoxLeft = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            playercontroller.isHitLeft = false;
            Debug.Log("hitcollider�߂���");
        }

        if (playercontroller.playerstate == PlayerController.PlayerState.Human)
        {
            if (collision.gameObject.tag == "Wall")
            {
                playercontroller.isHitLeft = false;
            }

            if (collision.gameObject.tag == "WoodBox")
            {
                playercontroller.hitWoodBoxLeft = false;
                playercontroller.pushstate = PlayerController.PushState.NotPushing;
            }
        }
    }
}
