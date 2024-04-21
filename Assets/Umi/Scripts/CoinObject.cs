using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// u.sasak
/// </summary>
public class CoinObject : MonoBehaviour
{
    [SerializeField, Header("コイン")] private GameObject[] coins;
    [SerializeField, Header("コインUI")] private Image[] imgCoin;

    void Start()
    {
    }

    void Update()
    {
        if (!coins[0].gameObject.activeSelf)
        {
            imgCoin[0].color += new Color(0, 0, 0, 255);
        }
        if (!coins[1].gameObject.activeSelf)
        {
            imgCoin[1].color += new Color(0, 0, 0, 255);
        }
        if (!coins[2].gameObject.activeSelf)
        {
            imgCoin[2].color += new Color(0, 0, 0, 255);
        }
    }
}
