using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private BoxCollider2D Bo2D;
    private PlayerController playercontroller;
    private HumanMotion humanMotion;
    private CapsuleMotion capsuleMotion;
    private Slope slope;
    // Start is called before the first frame update
    void Start()
    {
        Bo2D = GetComponent<BoxCollider2D>();
        playercontroller = FindObjectOfType<PlayerController>();
        humanMotion = FindObjectOfType<HumanMotion>();
        capsuleMotion = FindObjectOfType<CapsuleMotion>();
        slope = FindObjectOfType<Slope>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            if (playercontroller.isJump)
            {
                playercontroller.jumpSpeed = 0.0f;
                playercontroller.isGround = true;
                if (playercontroller.pushstate == PlayerController.PushState.Pushing)
                    humanMotion.HumanPushing();
                else
                {
                    humanMotion.HumanJumpFinish();
                }
                capsuleMotion.CapsuleJumpFinish();
                Debug.Log("’…’n‚µ‚½");
                playercontroller.isJump = false;
            }
        }
        if (collision.gameObject.tag == "WoodBox")
        {
            if (playercontroller.isJump)
            {
                playercontroller.jumpSpeed = 0.0f;
                playercontroller.isGround = true;
                humanMotion.HumanJumpFinish();
                capsuleMotion.CapsuleJumpFinish();
                playercontroller.isJump = false;
            }
            //if (playercontroller.speed == 0.0f)
            //{
            //    playermotion.StayMotion();
            //}
            //else
            //{
            //    playermotion.DashStartMotion();
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            playercontroller.isGround = false;
            playercontroller.isJump = true;
        }
        if (collision.gameObject.tag == "WoodBox")
        {
            playercontroller.isGround = false;
            playercontroller.isJump = true;
        }
    }
}
