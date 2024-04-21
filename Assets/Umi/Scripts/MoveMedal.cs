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
    [SerializeField, Header("メダル移動の速さX")] private float medalSpeedX = 2;
    [SerializeField,Header("メダル移動の速さY")] private float medalSpeedY = 1.5f;
    [SerializeField,Header("拡大の速さ")] private float sizeSpeed = 0.1f;
    float moveSpeed;
    [SerializeField,Header("メダルのサイズ")] private float medalSize = 0.01f;
    [SerializeField, Header("メダルの最大サイズ")] private float stopMedalSize = 0.1f;
    private GameObject player;
    [SerializeField,Header("クールタイム")] private float coolTime = 1.0f;
    bool tCheck;    //クールタイム確認

    [SerializeField, Header("遷移の時間")] private float SceneChangeTime = 2.0f;
    [SerializeField, Header("クリアテキスト")] private Image clearImage;
    [SerializeField] private GameObject effect;

    [SerializeField] private GameObject poseWindou;
    [SerializeField,Header("透過時間")] private float fadeTime = 2f;

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

        //上方向にメダルが拡大しながら移動する
        if (!tCheck)
        {
            medalSize += moveSpeed * Time.unscaledDeltaTime;
            //stopMedalSize以下だったら
            if (transform.localScale.x <= stopMedalSize)
            {
                Vector2 pos = new Vector2(0, medalSpeedY) * Time.unscaledDeltaTime;
                transform.Translate(pos);
            }
            //stopMedalSize以上だったら
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
        //プレイヤーに向かってメダルが縮小する
        if (tCheck)
        {
            Vector3 targeting = (player.transform.position - this.transform.position).normalized;
            transform.Translate(targeting * medalSpeedX * Time.unscaledDeltaTime);

            //縮小する
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

    //メダルが0より小さくなったらかつ
    //Stage3だったらClearシーンに遷移
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
