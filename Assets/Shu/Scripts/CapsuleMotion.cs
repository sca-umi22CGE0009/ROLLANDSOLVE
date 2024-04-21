using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;
using Spine;

public class CapsuleMotion : MonoBehaviour
{
    private PlayerController playercontroller;

    private string capsuleStay = "brething";
    private string capsuleStartDash = "turn";
    private string capsuleStartJump = "junp";
    private string capsuleJumping = "";
    private string capsuleJumpFinish = "landing";

    [SerializeField]
    public SkeletonAnimation capsuleAnimation;

    private Spine.AnimationState capsuleState;

    private TrackEntry stayEntry;
    private TrackEntry decelerationEntry;
    private TrackEntry dashTrackEntry;
    private TrackEntry jumpTrackEntry;
    private TrackEntry pushTrackEntry;
    private TrackEntry changeTrackEntry;
    private TrackEntry changeSceneEntry;
    // Start is called before the first frame update
    void Start()
    {
        playercontroller = GetComponent<PlayerController>();
        capsuleState = capsuleAnimation.AnimationState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CapsuleStop()
    {
        playercontroller.canMove = true;
    }

    public void CapsuleStay(TrackEntry stayEntry)
    {
        playercontroller.canMove = true;
        if (playercontroller.speed == 0.0f)
        {
            capsuleState.SetAnimation(0, capsuleStay, true);
        }
    }

    public void CapsuleDashStart()
    {
        dashTrackEntry = capsuleState.SetAnimation(0, capsuleStartDash, true);
        if (playercontroller.speed == 0.0f)
            dashTrackEntry.Complete += CapsuleStay;
    }

    public void CapsuleJumpStart()
    {
        jumpTrackEntry = capsuleState.SetAnimation(0, capsuleStartJump, false);
        jumpTrackEntry.Complete += CapsuleJumping;
    }

    public void CapsuleJumping(TrackEntry jumpTrackEntry)
    {
        playercontroller.jumpSpeed = playercontroller.CapsuleJumpPower;
        playercontroller.isJump = true;
    }

    public void CapsuleJumpFinish()
    {
        stayEntry = capsuleState.SetAnimation(0, capsuleJumpFinish, false);
        if (playercontroller.speed == 0.0f)
        {
            stayEntry.Complete += CapsuleStay;
        }
        else
        {
            CapsuleDashStart();
        }
    }

    public void CapsuleTimeScale()
    {
        capsuleAnimation.timeScale = 1 + (Mathf.Abs(playercontroller.speed) * 0.2f);
    }
}
