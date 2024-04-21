using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlueButtonPush : MonoBehaviour
{
    [SerializeField] GameObject BlueButton;
    [SerializeField, Header("ボタンを押したら表示")] public bool TileInstantiate;
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
    public bool isPush;
    public bool WoodBoxIsPush;

    void Start()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isPush)
        {
            BlueButton.transform.position = new Vector3(BlueButton.transform.position.x, BlueButton.transform.position.y - 0.25f, BlueButton.transform.position.z);
            isPush = true;
        }

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
        if (TileInstantiate)
            ActiveTile();
        else
            InactiveTile();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!WoodBoxIsPush)
        {
            if (isPush)
            {
                BlueButton.transform.position = new Vector3(BlueButton.transform.position.x, BlueButton.transform.position.y + 0.25f, BlueButton.transform.position.z);
                isPush = false;
            }

            foreach (var Object in InactiveObjects)
            {
                Object.SetActive(true);
            }
            foreach (var Object in ActiveObjects)
            {
                Object.SetActive(false);
            }
            if (tilemap == null || tile == null || !isPush)
                return;
            if (TileInstantiate)
                InactiveTile();
            else
                ActiveTile();
        }
    }

    public void ActiveTile()
    {
        for (int y = yMin; y < yMax+1; ++y)
        {
            for (int x = xMin; x < xMax+1; ++x)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }

    public void InactiveTile()
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
