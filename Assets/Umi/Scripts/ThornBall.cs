using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 担当: 佐々木海
/// </summary>
public class ThornBall : MonoBehaviour
{
    [SerializeField, Header("敵の移動速度")] private float Movespeed = 2f;
    private float enemySpeed;
    [SerializeField, Header("回転速度")] private float rotaSpeed = 200f;
    [SerializeField, Header("壁に当たった時の停止時間")] private float moveStopTime = 0.5f;
    //bool isVisible;
    bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        enemySpeed = Movespeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotaSpeed * Time.deltaTime);
        Vector2 pos = new Vector2(-enemySpeed, 0) * Time.deltaTime;
        transform.Translate(pos,Space.World);

        if (enemySpeed <= 0)
        {
            //Debug.Log("-");
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            StartCoroutine(MoveStop());
        }
    }

    private IEnumerator MoveStop()
    {
        float e = enemySpeed;
        float r = rotaSpeed; 
        enemySpeed = 0;
        rotaSpeed = 0;
        yield return new WaitForSeconds(moveStopTime);
        enemySpeed = e;
        rotaSpeed = r;
        enemySpeed *= -1.0f;
        rotaSpeed *= -1.0f;
    }

    ////Rendererが任意のカメラから見えると呼び出される
    //private void OnBecameVisible()
    //{
    //    isVisible = true;
    //}
    //private void OnBecameInvisible()
    //{
    //    isVisible = false;
    //}
}
