using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ’S“–: ²X–ØŠC
/// </summary>
public class ThornBall : MonoBehaviour
{
    [SerializeField, Header("“G‚ÌˆÚ“®‘¬“x")] private float Movespeed = 2f;
    private float enemySpeed;
    [SerializeField, Header("‰ñ“]‘¬“x")] private float rotaSpeed = 200f;
    [SerializeField, Header("•Ç‚É“–‚½‚Á‚½‚Ì’â~ŠÔ")] private float moveStopTime = 0.5f;
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
    }
    //•Ç‚ÉG‚ê‚½‚ç
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            StartCoroutine(MoveStop());
        }
    }
    //“®‚«‚ğ~‚ß‚é
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
}
