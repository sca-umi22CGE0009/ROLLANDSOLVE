using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SasakiShu
/// HP管理
/// </summary>
public class LifeManager : MonoBehaviour
{
    [SerializeField, Header("最終MAXHP")]
    private int maxHp;
    [SerializeField, Header("初期HP")]
    private int startLifePoint;

    public List<GameObject> HeartObjects;
    public Sprite HeartSprite;
    public Sprite NoHeartSprite;
    private int nowMaxHp;//現在の最大HP
    public int hp;//残りHP
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

    //ダメージを受けたとき
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
        //HPが0
        if (hp <= 0)
        {
            humanMotion.HumanDead();
        }
    }

    //回復
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

    //初期HPの設定
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

    //HPの上限値を増やすときに使う
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
