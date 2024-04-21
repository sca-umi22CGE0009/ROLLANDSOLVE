using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SasakiShu
/// プレイヤー操作
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField,Header("人型の速度上限")]
    private float humanLimitSpeed;
    [SerializeField, Header("カプセル型の速度上限")]
    public float CapsuleLimitSpeed;
    [SerializeField,Header("木箱を押し始めるまでの時間")]
    private float pushStartTime;
    [SerializeField,Header("木箱を押すときの速度")]
    private float pushSpeed;
    [SerializeField,Header("プレイヤー(人型)の加速度")]
    private float humanAcceleration;
    [SerializeField,Header("プレイヤー(人型)の減速度")]
    private float humanDeceleration;
    [SerializeField, Header("プレイヤー(カプセル型)の加速度")]
    private float capsuleAcceleration;
    [SerializeField, Header("プレイヤー(カプセル型)の減速度")]
    private float capsuleDeceleration;
    [SerializeField, Header("木箱に当たった時の減速度")]
    public float HitBoxDeceleration;
    [SerializeField, Header("敵に当たった時の減速度")]
    private float hitEnemyDeceleration;
    [SerializeField,Header("木箱を壊すときの速度割合(％)")]
    public float BreakProportion;
    [SerializeField, Header("跳ね返されるときの速度割合(％)")]
    private float backProportion;
    [SerializeField, Header("空中での移動割合(％)")]
    private float airMoveProportion;
    [SerializeField,Header("人型のジャンプ力")]
    public float HumanJumpPower;
    [SerializeField,Header("カプセル型のジャンプ力")]
    public float CapsuleJumpPower;
    [SerializeField,Header("重力")]
    private float gravityPower;
    [SerializeField,Header("形態変化のクールタイム")]
    private float coolTime;
    [SerializeField,Header("無敵時間の長さ")]
    private float invincibleTime;
    [SerializeField,Header("無敵時間後の微無敵時間")]
    private float afterInvincibleTime;

    [SerializeField]
    private GameObject groundCheck;
    [SerializeField]
    private GameObject capsuleObject;

    private CircleCollider2D co2D;
    private CapsuleCollider2D cap2D;
    private LifeManager lifeManager;
    private CameraController cameracontroller;
    private HumanMotion humanMotion;
    private CapsuleMotion capsuleMotion;
    private Slope slope;
    private HitLeftCheck hitLeftCheck;
    private HitRightCheck hitRightCheck;
    private MoveEnemy moveEnemy;
    private float limitSpeed; // 速度上限保管の変数
    private float acceleration; // 加速度保管の変数
    private float deceleration; // 減速度保管の変数
    private float countDownCoolTime; // 変身のクールタイム
    private float toPush; // 木箱を押す時間
    private float iTime; // invincibleTimeの保管
    private float aITime; // afterinvincibleTimeの保管
    private float blinkingTime = 0.15f;//無敵時の点滅時間
    private float enemyHitShake = 0.04f;
    private bool isPush = false;
    private bool alpha = false;//透明か見えているか

    public enum PlayerState { Human, Capsule }
    public PlayerState playerstate; //プレイヤーの形態
    public enum DirectionState { Right, Left }
    public DirectionState directionstate; //プレイヤーの進んでいる向き
    public enum PushState { NotPushing, StartPush, Pushing}
    public PushState pushstate;　//木箱を押している状態
    public enum LastKeyState { Left, Right}
    public LastKeyState lastKeyState;
    public GameObject HitCheckRight;
    public GameObject HitCheckLeft;
    public Renderer HumanFront;
    public Renderer HumanBack;
    public Renderer Capsule;
    public float speed;//プレイヤーの移動速度
    public float jumpSpeed;//ジャンプ時のy軸移動速度
    public float SlopeAngleX = 1.0f; // 傾斜のX軸移動量
    public float SlopeAngleY = 0.0f; // 傾斜のY軸移動量
    public float SlopeAngle; // stageの傾斜
    public float ObjectHitShake = 0.02f;
    public bool isInvincible = false;//無敵か
    public bool isJump = true;//ジャンプ中か
    public bool isGround;//地面に接しているか
    public bool canMove = true;//操作可能か
    public bool isHitRight;//右側が当たったか
    public bool isHitLeft;//左側が当たったか
    public bool hitWoodBoxRight = false;//木箱が右側に当たったか
    public bool hitWoodBoxLeft = false;//木箱が左側に当たったか


    void Start()
    {
        co2D = GetComponent<CircleCollider2D>();
        cap2D = GetComponent<CapsuleCollider2D>();
        humanMotion = GetComponent<HumanMotion>();
        capsuleMotion = GetComponent<CapsuleMotion>();
        lifeManager = FindObjectOfType<LifeManager>();
        cameracontroller = FindObjectOfType<CameraController>();
        slope = FindObjectOfType<Slope>();
        hitLeftCheck = FindObjectOfType<HitLeftCheck>();
        hitRightCheck = FindObjectOfType<HitRightCheck>();

        playerstate = PlayerState.Human;
        directionstate = DirectionState.Right;
        co2D.enabled = false;
        cap2D.enabled = true;
        HumanBack.enabled = false;
        Capsule.enabled = false;
        countDownCoolTime = coolTime;
        limitSpeed = humanLimitSpeed;
        acceleration = humanAcceleration * 300;
        deceleration = humanDeceleration * 300;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Debug.Log(directionstate);
        }
        if (canMove)
        {
            if (isPush && playerstate == PlayerState.Human)
                Pushing();
            else
                PlayerMove();

            PlayerJump();
        }
        Gravity();
        if (isInvincible)
            Invincible(); // 無敵

        ChangeController();
    }

    private void PlayerMove()
    {
        //最高速度を制限
        if (speed >= limitSpeed)
        {
            speed = limitSpeed;
        }
        else if (-speed >= limitSpeed)
        {
            speed = -limitSpeed;
        }

        //ジャンプ中の移動
        if (isJump)
        {
            transform.position += new Vector3(speed, jumpSpeed, 0) * Time.deltaTime;
            if (Input.GetKey(KeyCode.D))
            {
                lastKeyState = LastKeyState.Right;
                if (!isHitRight)
                {
                    speed += (acceleration * (airMoveProportion / 100)) / 1000 * Time.deltaTime;
                    if (directionstate == DirectionState.Left)
                    {
                        if (speed > 0.0f)
                        {
                            speed = 0.0f;
                        }
                    }
                }
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                lastKeyState = LastKeyState.Left;
                if (!isHitLeft)
                {
                    speed -= (acceleration * (airMoveProportion / 100)) / 1000 * Time.deltaTime;
                    if (directionstate == DirectionState.Right)
                    {
                        if (speed < 0.0f)
                        {
                            speed = 0.0f;
                        }
                    }
                }
            }
        }

        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                ChangeDirection();
                lastKeyState = LastKeyState.Right;
                directionstate = DirectionState.Right;
                if (hitWoodBoxRight)
                {
                    //ChangeDirection();
                    Pushing();
                }
                else if (!isHitRight)
                {
                    if (directionstate == DirectionState.Left)
                    {
                        humanMotion.HumanDashStart();
                        capsuleMotion.CapsuleDashStart();
                    }
                    if (speed == 0.0f)
                    {
                        humanMotion.HumanDashStart();
                        capsuleMotion.CapsuleDashStart();
                    }

                    transform.position += new Vector3(speed * SlopeAngleX, (SlopeAngle * speed) * SlopeAngleY, 0) * Time.deltaTime;
                    speed += acceleration / 1000 * Time.deltaTime;
                }
                if (hitWoodBoxLeft)
                {
                    toPush = pushStartTime; //押す時間リセット
                    if (pushstate != PushState.NotPushing)
                    {
                        pushstate = PushState.NotPushing;
                    }
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                ChangeDirection();
                lastKeyState = LastKeyState.Left;
                directionstate = DirectionState.Left;
                if (hitWoodBoxLeft)
                {
                    //ChangeDirection();
                    Pushing();
                }
                else if (!isHitLeft)
                {
                    if (directionstate == DirectionState.Right)
                    {
                        humanMotion.HumanDashStart();
                        capsuleMotion.CapsuleDashStart();
                    }
                    if (speed == 0.0f)
                    {
                        humanMotion.HumanDashStart();
                        capsuleMotion.CapsuleDashStart();
                    }

                    transform.position += new Vector3(speed * SlopeAngleX, (SlopeAngle * speed) * SlopeAngleY, 0) * Time.deltaTime;
                    speed -= acceleration / 1000 * Time.deltaTime;
                }
                if (hitWoodBoxRight)
                {
                    toPush = pushStartTime; //押す時間リセット
                    if (pushstate != PushState.NotPushing)
                    {
                        pushstate = PushState.NotPushing;
                    }
                }
            }
            else
            {
                transform.position += new Vector3(speed * SlopeAngleX, (SlopeAngle * speed) * SlopeAngleY, 0) * Time.deltaTime;

                if (speed < 0.0f)
                {
                    speed += deceleration / 1000 * Time.deltaTime;
                    if (speed > 0.0f)
                    {
                        speed = 0.0f;
                        humanMotion.HumanStop();
                        capsuleMotion.CapsuleStop();
                    }
                }
                else if (speed > 0.0f)
                {
                    speed -= deceleration / 1000 * Time.deltaTime;
                    if (speed < 0.0f)
                    {
                        speed = 0.0f;
                        humanMotion.HumanStop();
                        capsuleMotion.CapsuleStop();
                    }
                }
                toPush = pushStartTime; //押す時間リセット
                if (pushstate != PushState.NotPushing)
                {
                    pushstate = PushState.NotPushing;
                    humanMotion.HumanStop();
                    capsuleMotion.CapsuleStop();
                }
            }
        }
        if (playerstate == PlayerState.Capsule)
        {
            humanMotion.HumanTimeScale();
            capsuleMotion.CapsuleTimeScale();
        }
    }

    private void PlayerJump() {
        if(!isJump && Input.GetKeyDown(KeyCode.Space))
        {
            if (playerstate == PlayerState.Human)
            {
                humanMotion.HumanJumpStart();
            }
            else if (playerstate == PlayerState.Capsule)
            {
                capsuleMotion.CapsuleJumpStart();
            }
        }
    }

    private void Pushing()
    {
        toPush -= Time.deltaTime;
        if (toPush > 0.0f)
        {
            if (pushstate == PushState.NotPushing)
            {
                pushstate = PushState.StartPush;
                humanMotion.HumanPushStart();
            }
            speed = 0.0f;
        }
        else
        {
            if (pushstate == PushState.StartPush)
            {
                pushstate = PushState.Pushing;
                humanMotion.HumanPushing();
            }
            if (hitWoodBoxRight)
            {
                speed = pushSpeed;
                transform.position += new Vector3(speed * SlopeAngleX, (SlopeAngle * speed) * SlopeAngleY, 0) * Time.deltaTime;
            }
            else if (hitWoodBoxLeft)
            {
                speed = -pushSpeed;
                transform.position += new Vector3(speed * SlopeAngleX, (SlopeAngle * speed) * SlopeAngleY, 0) * Time.deltaTime;
            }
            toPush = 0.0f;
        }
    }

    public void BounceBack() //ぶつかったときの跳ね返り
    {
        speed = -speed * (backProportion / 100);
    }

    private void Gravity() 
    {
        if (isJump || !isGround)
        {
            jumpSpeed -= gravityPower * Time.deltaTime;
            if (transform.position.y <= -10.0f)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    private void ChangeController() //変身のクールタイムやボタンなど
    {
        countDownCoolTime -= Time.deltaTime;
        if (countDownCoolTime < 0.0f)
        {
            countDownCoolTime = 0.0f;
        }
        if (!isJump || isGround)
        {
            if (countDownCoolTime <= 0.0f && !isInvincible)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    countDownCoolTime = coolTime;
                    if (playerstate == PlayerState.Human)
                        humanMotion.HumanChange();
                    else if (playerstate == PlayerState.Capsule)
                        Change();
                }
            }
        }
    }

    public void Change() //変身時の見た目変更など
    {
        //カプセル型にCHANGE
        if (playerstate != PlayerState.Capsule)
        {
            playerstate = PlayerState.Capsule;
            co2D.enabled = true;
            cap2D.enabled = false;
            HumanFront.enabled = false;
            HumanBack.enabled = false;
            hitWoodBoxRight = false;
            hitWoodBoxLeft = false;
            //if (directionstate == DirectionState.Right)
            //    transform.position = new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z);
            //else if (directionstate == DirectionState.Left)
            //    transform.position = new Vector3(transform.position.x - 0.4f, transform.position.y, transform.position.z);
            HitCheckRight.transform.position = new Vector3(transform.position.x + 0.57f, HitCheckRight.transform.position.y , 0.0f);
            HitCheckLeft.transform.position = new Vector3(transform.position.x - 0.57f, HitCheckLeft.transform.position.y, 0.0f);
            //cameracontroller.cameraMoveState = CameraController.CameraMoveState.Turn;
            Capsule.enabled = true;
            if (isPush)
            {
                speed = 0.0f;
            }
            isPush = false;
            limitSpeed = CapsuleLimitSpeed;
            acceleration = capsuleAcceleration * 300;
            deceleration = capsuleDeceleration * 300;
        }

        //人型にCHANGE
        else if (playerstate != PlayerState.Human)
        {
            playerstate = PlayerState.Human;
            Capsule.enabled = false;
            if (directionstate == DirectionState.Right)
                HumanFront.enabled = true;
            else if (directionstate == DirectionState.Left)
                HumanBack.enabled = true;
            cap2D.enabled = true;
            co2D.enabled = false;
            HitCheckRight.transform.position = new Vector3(transform.position.x + 0.45f, HitCheckRight.transform.position.y, 0.0f);
            HitCheckLeft.transform.position = new Vector3(transform.position.x - 0.45f, HitCheckLeft.transform.position.y, 0.0f);
            humanMotion.HumanTimeScale();
            limitSpeed = humanLimitSpeed;
            acceleration = humanAcceleration * 300;
            deceleration = humanDeceleration * 300;
        }
    }

    private void Invincible()
    {
        Physics2D.IgnoreLayerCollision(3, 7, true);
        iTime -= Time.deltaTime;
        blinkingTime -= Time.deltaTime;
        if (blinkingTime <= 0.0f)
        {
            if (alpha != true)
                alpha = true;
            else
                alpha = false;
            if (alpha)
            {
                HumanFront.enabled = false;
                HumanBack.enabled = false;
            }
            else
            {
                if (directionstate == DirectionState.Right)
                    HumanFront.enabled = true;
                else if (directionstate == DirectionState.Left)
                    HumanBack.enabled = true;
            }
            blinkingTime = 0.15f;
        }

        if (iTime <= 0.0f)
        {
            aITime -= Time.deltaTime;
            if (directionstate == DirectionState.Right)
                HumanFront.enabled = true;
            else if (directionstate == DirectionState.Left)
                HumanBack.enabled = true;
            iTime = 0.0f;
            if (aITime <= 0.0f)
            {
                isInvincible = false;
                Physics2D.IgnoreLayerCollision(3, 7, false);
            }
        }
    }

    private void ChangeDirection()
    {
        if (lastKeyState == LastKeyState.Left)
        {
            if (playerstate == PlayerState.Human)
            {
                HumanFront.enabled = false;
                HumanBack.enabled = true;
            }
            else if (playerstate == PlayerState.Capsule)
            {
                capsuleObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else if(lastKeyState == LastKeyState.Right)
        {
            if (playerstate == PlayerState.Human)
            {
                HumanBack.enabled = false;
                HumanFront.enabled = true;
            }
            else if(playerstate == PlayerState.Capsule)
            {
                capsuleObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void PlayerDamage()
    {
        if (!isInvincible)
        {
            canMove = false;
            if (playerstate == PlayerState.Capsule)
            {
                Change();
            }
            if (lifeManager.hp > 0)
            {
                humanMotion.HumanDamage();
            }
            lifeManager.Damage();
            if (lifeManager.hp > 0)
                isInvincible = true;
            speed = 0.0f;
            cameracontroller.HitShake( 0.25f, enemyHitShake);
            speed = speed * ((100 - hitEnemyDeceleration) / 100);
            iTime = invincibleTime;
            aITime = afterInvincibleTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            if (isJump)
            {
                foreach(ContactPoint2D point in collision.contacts)
                {
                    Vector2 relativePoint = transform.InverseTransformPoint(point.point);

                    if (relativePoint.x > 0.2f)
                    {
                        Debug.Log("空中で右側がぶつかった");
                        hitRightCheck.PlayerStop();
                    }
                    else if(relativePoint.x < -0.2f)
                    {
                        Debug.Log("空中で左側がぶつかった");
                        hitLeftCheck.PlayerStop();
                    }
                    if (relativePoint.y > 0.2f)
                    {
                        Debug.Log("頭がぶつかった");
                        jumpSpeed = 0.0f;
                    }
                }
            }
            else
            {
                if (playerstate == PlayerState.Human)
                {
                    foreach (ContactPoint2D point in collision.contacts)
                    {
                        Vector2 relativePoint = transform.InverseTransformPoint(point.point);

                        if (relativePoint.x > 0.2f && relativePoint.y > 0.1f)
                        {
                            hitRightCheck.PlayerStop();
                        }
                        else if (relativePoint.x < -0.2f && relativePoint.y > 0.1f)
                        {
                            hitLeftCheck.PlayerStop();
                        }
                    }
                }
            }
        }
        if (collision.gameObject.tag == "WoodBox")
        {
            if (playerstate == PlayerState.Capsule)
            {
                if (Mathf.Abs(speed) >= CapsuleLimitSpeed * (BreakProportion / 100))
                {
                    speed = speed * ((100 - HitBoxDeceleration)/100);
                    cameracontroller.HitShake(0.25f, ObjectHitShake);
                    //木箱が壊れる、消える
                    Destroy(collision.gameObject);
                }
                else
                {
                    BounceBack();
                }
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            if (playerstate == PlayerState.Capsule)
            {
                if (Mathf.Abs(speed) >= CapsuleLimitSpeed * ((BreakProportion-25) / 100))
                {
                    speed = speed * ((100 - HitBoxDeceleration) / 100);
                    cameracontroller.HitShake(0.25f, 0.05f);
                    //木が壊れる、消える
                    Destroy(collision.gameObject);
                }
                else
                {
                    //跳ね返る
                    cameracontroller.HitShake(0.25f, ObjectHitShake);
                    BounceBack();
                    Debug.Log("壁に当たった");
                }
            }
        }
        if (collision.gameObject.tag == "Heart")
        {
            Destroy(collision.gameObject);
            lifeManager.HeartRecovery();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D point in collision.contacts)
        {
            Vector2 relativePoint = transform.InverseTransformPoint(point.point);

            if (relativePoint.y < 0.0f && relativePoint.x > -0.3f && relativePoint.x < 0.3f)
            {
                Debug.Log("床に触れた");
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            }
        }
        if (playerstate == PlayerState.Human)
        {
            if (collision.gameObject.tag == "WoodBox")
            {
                if (isJump)
                {
                    speed = 0.0f;
                }
            }
        }
        if (playerstate == PlayerState.Capsule)
        {
            if (collision.gameObject.tag == "WoodBox")
            {
                if (Mathf.Abs(speed) <= 0.5f)
                {
                    speed = 0.0f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (playerstate == PlayerState.Human)
            {
                PlayerDamage();
            }
            else if (playerstate == PlayerState.Capsule)
            {
                if (Mathf.Abs(speed) > limitSpeed * 0.5f)
                {
                    //ここに吹き飛ばす関数
                    moveEnemy = collision.gameObject.GetComponent<MoveEnemy>();

                    moveEnemy.BlowAway();
                    //Destroy(collision.gameObject);
                }
                else
                    PlayerDamage();
            }
        }
        if (collision.gameObject.tag == "Thorn")
        {
            PlayerDamage();
        }
    }
}