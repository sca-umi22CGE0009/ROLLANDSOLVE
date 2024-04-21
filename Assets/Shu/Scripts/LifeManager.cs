using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SasakiShu
/// HP�Ǘ�
/// </summary>
public class LifeManager : MonoBehaviour
{
    [SerializeField, Header("�ŏIMAXHP")]
    private int maxHp;
    [SerializeField, Header("����HP")]
    private int startLifePoint;

    public List<GameObject> HeartObjects;
    public Sprite HeartSprite;
    public Sprite NoHeartSprite;
    private int nowMaxHp;//���݂̍ő�HP
    public int hp;//�c��HP
    private int lifeCount;
    private HumanMotion humanMotion;

    void Start()
    {
        humanMotion = FindObjectOfType<HumanMotion>();
        nowMaxHp = startLifePoint;
        hp = startLifePoint;
        StartHp();
    }

    void Update()
    {

    }

    //�_���[�W���󂯂��Ƃ�
    public void Damage()
    {
        foreach (var heart in HeartObjects)
        {
            if (hp > 0)
            {
                if (heart.activeSelf)
                {
                    if (heart.GetComponent<Image>().sprite == HeartSprite)
                    {
                        hp -= 1;
                        heart.GetComponent<Image>().sprite = NoHeartSprite;
                        break;
                    }
                }
            }
        }
        //HP��0
        if (hp <= 0)
        {
            humanMotion.HumanDead();
        }
    }

    //��
    public void HeartRecovery()
    {
        HeartObjects.Reverse();
        foreach (var heart in HeartObjects)
        {
            if (hp < nowMaxHp)
            {
                if (heart.GetComponent<Image>().sprite == NoHeartSprite)
                {
                    hp += 1;
                    heart.GetComponent<Image>().sprite = HeartSprite;
                    break;
                }
            }
        }
        HeartObjects.Reverse();
    }

    //����HP�̐ݒ�
    private void StartHp()
    {
        foreach (var heart in HeartObjects)
        {
            heart.SetActive(false);
            lifeCount++;
            if (lifeCount == maxHp - startLifePoint)
            {
                break;
            }
        }
    }

    //HP�̏���l�𑝂₷�Ƃ��Ɏg��
    private void MaxHpUp()
    {
        nowMaxHp += 1;
        HeartObjects.Reverse();
        foreach (var heart in HeartObjects)
        {
            if (!heart.activeSelf)
            {
                heart.SetActive(true);
                heart.GetComponent<Image>().sprite = NoHeartSprite;
                break;
            }
        }
        HeartObjects.Reverse();
    }
}
