using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// u.sasak
/// </summary>
public class SmallTreasureChest : MonoBehaviour
{
    [SerializeField] private GameObject medal;
    [SerializeField] private GameObject chest;
    //[SerializeField] private GameObject sChest;
    private Animator OpenAnime;
    private PlayerController playerController;

    bool isTouch = false;
    // Start is called before the first frame update
    void Start()
    {
        OpenAnime = GetComponent<Animator>();
        //宝箱が消える現象が出たため
        chest.gameObject.SetActive(true);
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが宝箱に触ったらプレイヤーの速度を0にし
        //アニメーション開始
        if (isTouch)
        {
            playerController.speed = 0;
            OpenAnime.SetBool("IsOpen", true);
            medal.gameObject.SetActive(true);
        }
    }
    //プレイヤーに触ってる
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerController.playerstate == PlayerController.PlayerState.Human)
                isTouch = true;
        }
    }
}
