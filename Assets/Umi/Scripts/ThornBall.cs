using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �S��: ���X�؊C
/// </summary>
public class ThornBall : MonoBehaviour
{
    [SerializeField, Header("�G�̈ړ����x")] private float Movespeed = 2f;
    private float enemySpeed;
    [SerializeField, Header("��]���x")] private float rotaSpeed = 200f;
    [SerializeField, Header("�ǂɓ����������̒�~����")] private float moveStopTime = 0.5f;
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
    //�ǂɐG�ꂽ��
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            StartCoroutine(MoveStop());
        }
    }
    //�������~�߂�
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
