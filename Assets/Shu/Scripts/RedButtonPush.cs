using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RedButtonPush : MonoBehaviour
{
    [SerializeField] GameObject RedButton;
    [SerializeField,Header("ボタンを押したら表示")] bool instantiate;
    [SerializeField, Header("表示するオブジェクト")]
    private GameObject[] ActiveObjects;
    [SerializeField, Header("非表示にするオブジェクト")]
    private GameObject[] InactiveObjects;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tile tile;
    [SerializeField] int yMin;
    [SerializeField] int yMax;
    [SerializeField] int xMin;
    [SerializeField] int xMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("踏まれた");
        RedButton.transform.position = new Vector3(RedButton.transform.position.x, RedButton.transform.position.y - 0.1f, RedButton.transform.position.z);

        foreach (var Object in InactiveObjects)
        {
            Object.SetActive(false);
        }
        foreach (var Object in ActiveObjects)
        {
            Object.SetActive(true);
        }
        if (tilemap == null || tile == null)
            return;
        if (instantiate)
            ActiveTile();
        else
            InactiveTile();
    }

    private void ActiveTile()
    {
        for (int y = yMin; y < yMax + 1; ++y)
        {
            for (int x = xMin; x < xMax + 1; ++x)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }

    private void InactiveTile()
    {
        for (int y = yMin; y < yMax + 1; ++y)
        {
            for (int x = xMin; x < xMax + 1; ++x)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), null);
            }
        }
    }
}
