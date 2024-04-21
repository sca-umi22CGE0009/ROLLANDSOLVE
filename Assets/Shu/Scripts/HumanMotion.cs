using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;
using Spine;

public class HumanMotion : MonoBehaviour
{
    private PlayerController playercontroller;

    private string humanStartDash = "dash_motion";
    private string humanDash = "dash";
    private string humanStay = "idoling";
    private string humanStartJump = "jamp_motion";
    private string humanJumping = "jamping";
    private string humanJumpFinish = "landing";
    private string humanStartPush = "push_motion";
    private string humanPushing = "push";
    private string humanChange = "change";
    private string humanDamage = "damage";
    private string humanDead = "dead";

    [SerializeField]
    private SkeletonAnimation humanFrontAnimation;
    [SerializeField]
    private SkeletonAnimation humanBackAnimation;

    private Spine.AnimationState humanFrontState;
    private Spine.AnimationState humanBackState;

    private TrackEntry stayEntry;
    private TrackEntry decelerationEntry;
    private TrackEntry dashTrackEntry;
    private TrackEntry jumpTrackEntry;
    private TrackEntry pushTrackEntry;
    private TrackEntry changeTrackEntry;
    private TrackEntry changeSceneEntry;

    private bool isChange = false;
    // Start is called before the first frame update
    void Start()
    {
        playercontroller = GetComponent<PlayerController>();
        humanFrontState = humanFrontAnimation.AnimationState;
        humanBackState = humanBackAnimation.AnimationState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HumanStop()
    {
        playercontroller.canMove = true;
        humanFrontState.SetAnimation(0, humanStay, true);
        humanBackState.SetAnimation(0, humanStay, true);
    }

    public void HumanStay(TrackEntry stayEntry)
    {
        playercontroller.canMove = true;
        humanFrontState.SetAnimation(0, humanStay, true);
        humanBackState.SetAnimation(0, humanStay, true);
    }

    public void HumanDashStart()
    {
        if (!isChange)
        {
            HumanTimeScale();
            dashTrackEntry = humanFrontState.SetAnimation(0, humanStartDash, false);
            dashTrackEntry = humanBackState.SetAnimation(0, humanStartDash, false);
            dashTrackEntry.Complete += HumanDash;
        }
    }

    public void HumanDash(TrackEntry dashTrackEntry)
    {
        HumanTimeScale();
        humanFrontState.SetAnimation(0, humanDash, true);
        humanBackState.SetAnimation(0, humanDash, true);
        if (playercontroller.speed == 0.0f)
        {
            HumanStop();
        }
    }

    public void HumanJumpStart()
    {
        if (!isChange)
        {
            humanFrontAnimation.timeScale = 5.0f;
            humanBackAnimation.timeScale = 5.0f;
            jumpTrackEntry = humanFrontState.SetAnimation(0, humanStartJump, false);
            jumpTrackEntry = humanBackState.SetAnimation(0, humanStartJump, false);
            jumpTrackEntry.Complete += HumanJumping;
        }
    }

    public void HumanJumping(TrackEntry jumpTrackEntry)
    {
        HumanTimeScale();
        playercontroller.jumpSpeed = playercontroller.HumanJumpPower;
        playercontroller.isJump = true;
        humanFrontState.SetAnimation(0, humanJumping, false);
        humanBackState.SetAnimation(0, humanJumping, false);
    }

    public void HumanJumpFinish()
    {
        if (playercontroller.playerstate == PlayerController.PlayerState.Human)
        {
            stayEntry = humanFrontState.SetAnimation(0, humanJumpFinish, false);
            stayEntry = humanBackState.SetAnimation(0, humanJumpFinish, false);
            //if (playercontroller.Slope != 0.0f)
            //{
            //    stayEntry.Complete += HumanDash;
            //}
            if (playercontroller.speed == 0.0f)
            {
                stayEntry.Complete += HumanStay;
            }
            else
            {
                HumanDashStart();
            }
        }
    }

    public void HumanPushStart()
    {
        humanFrontState.SetAnimation(0, humanStartPush, false);
        humanBackState.SetAnimation(0, humanStartPush, false);
    }

    public void HumanPushing()
    {
        humanFrontState.SetAnimation(0, humanPushing, true);
        humanBackState.SetAnimation(0, humanPushing, true);
    }

    public void HumanChange()
    {
        isChange = true;
        humanFrontAnimation.timeScale = 3.0f;
        humanBackAnimation.timeScale = 3.0f;
        changeTrackEntry = humanFrontState.SetAnimation(0, humanChange, false);
        changeTrackEntry = humanBackState.SetAnimation(0, humanChange, false);
        changeTrackEntry.Complete += HumanAfterChange;
    }

    public void HumanAfterChange(TrackEntry changeTrackEntry)
    {
        Debug.Log("ïœêg");
        humanFrontState.SetAnimation(0, humanStay, true);
        humanBackState.SetAnimation(0, humanStay, true);
        playercontroller.Change();
        isChange = false;
    }

    public void HumanDamage()
    {
        HumanTimeScale();
        stayEntry = humanFrontState.SetAnimation(0, humanDamage, false);
        stayEntry = humanBackState.SetAnimation(0, humanDamage, false);
        stayEntry.Complete += HumanStay;
    }

    public void HumanDead()
    {
        Debug.Log("HPÇ™0Ç…Ç»Ç¡ÇΩ");
        changeSceneEntry = humanFrontState.SetAnimation(0, humanDead, false);
        changeSceneEntry = humanBackState.SetAnimation(0, humanDead, false);
        changeSceneEntry.Complete += ChangeScene;
    }

    public void HumanTimeScale()
    {
        humanFrontAnimation.timeScale = 1;
        humanBackAnimation.timeScale = 1;
    }

    private void ChangeScene(TrackEntry changeSceneEntry)
    {
        SceneManager.LoadScene("GameOver");
    }
}
