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
        //�󔠂������錻�ۂ��o������
        chest.gameObject.SetActive(true);
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[���󔠂ɐG������v���C���[�̑��x��0�ɂ�
        //�A�j���[�V�����J�n
        if (isTouch)
        {
            playerController.speed = 0;
            OpenAnime.SetBool("IsOpen", true);
            medal.gameObject.SetActive(true);
        }
    }
    //�v���C���[�ɐG���Ă�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerController.playerstate == PlayerController.PlayerState.Human)
                isTouch = true;
        }
    }
}
