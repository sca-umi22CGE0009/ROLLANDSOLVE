using System.Collections;

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// u.sasak
/// </summary>
public class MoveEnemy : MonoBehaviour
{
    [SerializeField,Header("�G�̈ړ����x")] private float moveSpeed = 1f;
    [SerializeField,Header("��ԑ��x")] private float desSpeed = 5f;
    [SerializeField, Header("�v���C���[���߂Â������ɔ͈�")] private float enemyRange = 3f;
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

        //�E�΂ߏ�ɔ��ł���
        if (DamegeCheck)
        {
            Vector2 dPos = new Vector2(desSpeed, desSpeed) * Time.deltaTime;
            transform.Translate(dPos);
        }
    }

    //�v���C���[�̔���
    private void onPlayerRange()
    {
        //Vector3.Distance = ��_�Ԃ̋��������߂���
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        //�v���C���[���G���enemyRange�ȏ㗣�ꂽ��ǂ�������̂���߂�
        if (dist >= enemyRange|| dist <= -enemyRange)
        {
            //�~�܂�
            anim.SetBool("isWalk", false);
            enemySpeed = 0;
        }
        else
        {
            anim.SetBool("isWalk", true);
            inMonsterRange();
        }
    }

    //�v���C���[���߂Â�����ǂ�������
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

    //�_���[�W���󂯂���
    public void BlowAway()
    {
        DamegeCheck = true;

        anim.SetBool("isDamage", true);
        this.bc.isTrigger = true;
        this.rb.gravityScale = 0;

        Destroy(this.gameObject, 3.0f);
    }
}
