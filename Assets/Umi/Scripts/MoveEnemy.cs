using System.Collections;

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// u.sasak
/// </summary>
public class MoveEnemy : MonoBehaviour
{
    [SerializeField,Header("敵の移動速度")] private float moveSpeed = 1f;
    [SerializeField,Header("飛ぶ速度")] private float desSpeed = 5f;
    [SerializeField, Header("プレイヤーが近づいた時に範囲")] private float enemyRange = 3f;
    private GameObject player;
    private float enemySpeed;
    
    [SerializeField] private Animator anim;
    private BoxCollider2D bc;
    private Rigidbody2D rb;

    bool DamegeCheck = false;
    void Start()
    {
        enemySpeed = moveSpeed;
        player = GameObject.FindWithTag("Player");
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 pos = new Vector2(-enemySpeed, 0) * Time.deltaTime;
        transform.Translate(pos);
        onPlayerRange();

        //右斜め上に飛んでいく
        if (DamegeCheck)
        {
            Vector2 dPos = new Vector2(desSpeed, desSpeed) * Time.deltaTime;
            transform.Translate(dPos);
        }
    }

    //プレイヤーの判定
    private void onPlayerRange()
    {
        //Vector3.Distance = 二点間の距離を求められる
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        //プレイヤーが敵よりenemyRange以上離れたら追いかけるのをやめる
        if (dist >= enemyRange|| dist <= -enemyRange)
        {
            //止まる
            anim.SetBool("isWalk", false);
            enemySpeed = 0;
        }
        else
        {
            anim.SetBool("isWalk", true);
            inMonsterRange();
        }
    }

    //プレイヤーが近づいたら追いかける
    private void inMonsterRange()
    {
        Vector2 targeting = (player.transform.position -
                                this.transform.position).normalized;
        enemySpeed = (-moveSpeed / 2) * targeting.x;

        Vector2 lscale = gameObject.transform.localScale;
        if (lscale.x < 0 && targeting.x <= 0)
        {
            lscale.x *= -1;
            gameObject.transform.localScale = lscale;
        }
        else if (lscale.x > 0 && targeting.x >= 0)
        {
            lscale.x *= -1;
            gameObject.transform.localScale = lscale;
        }
    }

    //ダメージを受けたら
    public void BlowAway()
    {
        DamegeCheck = true;

        anim.SetBool("isDamage", true);
        this.bc.isTrigger = true;
        this.rb.gravityScale = 0;

        Destroy(this.gameObject, 3.0f);
    }
}
