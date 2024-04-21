using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// u.sasak
/// </summary>
public class MoveMedal : MonoBehaviour
{
    [SerializeField, Header("���_���ړ��̑���X")] private float medalSpeedX = 2;
    [SerializeField,Header("���_���ړ��̑���Y")] private float medalSpeedY = 1.5f;
    [SerializeField,Header("�g��̑���")] private float sizeSpeed = 0.1f;
    float moveSpeed;
    [SerializeField,Header("���_���̃T�C�Y")] private float medalSize = 0.01f;
    [SerializeField, Header("���_���̍ő�T�C�Y")] private float stopMedalSize = 0.1f;
    private GameObject player;
    [SerializeField,Header("�N�[���^�C��")] private float coolTime = 1.0f;
    bool tCheck;    //�N�[���^�C���m�F

    [SerializeField, Header("�J�ڂ̎���")] private float SceneChangeTime = 2.0f;
    [SerializeField, Header("�N���A�e�L�X�g")] private Image clearImage;
    [SerializeField] private GameObject effect;

    [SerializeField] private GameObject poseWindou;
    [SerializeField,Header("���ߎ���")] private float fadeTime = 2f;

    bool alphaCheck = false;
    float alpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        medalSize = 0;
        moveSpeed = sizeSpeed;
        tCheck = false;
        poseWindou.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(medalSize, medalSize);
        effect.transform.localScale = transform.localScale;

        //������Ƀ��_�����g�債�Ȃ���ړ�����
        if (!tCheck)
        {
            medalSize += moveSpeed * Time.unscaledDeltaTime;
            //stopMedalSize�ȉ���������
            if (transform.localScale.x <= stopMedalSize)
            {
                Vector2 pos = new Vector2(0, medalSpeedY) * Time.unscaledDeltaTime;
                transform.Translate(pos);
            }
            //stopMedalSize�ȏゾ������
            else if (transform.localScale.x >= stopMedalSize)
            {
                coolTime -= Time.unscaledDeltaTime;
                moveSpeed = 0;

                if (coolTime <= 0)
                {
                    tCheck = true;
                }
            }
        }
        //�v���C���[�Ɍ������ă��_�����k������
        if (tCheck)
        {
            Vector3 targeting = (player.transform.position - this.transform.position).normalized;
            transform.Translate(targeting * medalSpeedX * Time.unscaledDeltaTime);

            //�k������
            moveSpeed = sizeSpeed;
            medalSize -= moveSpeed * Time.unscaledDeltaTime;
            if (medalSize < 0)
            {
                medalSize = 0;
                clearImage.gameObject.SetActive(true);
                StartCoroutine(WaitTime());
            }
        }
    }

    //���_����0��菬�����Ȃ����炩��
    //Stage3��������Clear�V�[���ɑJ��
    private IEnumerator WaitTime()
    {
        yield return new WaitForSecondsRealtime(SceneChangeTime);
        SceneManager.LoadScene("SelectScene");
        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            SceneManager.LoadScene("Clear");
        }
    }
}
