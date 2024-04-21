using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RedButtonPush : MonoBehaviour
{
    [SerializeField] GameObject RedButton;
    [SerializeField,Header("�{�^������������\��")] bool instantiate;
    [SerializeField, Header("�\������I�u�W�F�N�g")]
    private GameObject[] ActiveObjects;
    [SerializeField, Header("��\���ɂ���I�u�W�F�N�g")]
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
        Debug.Log("���܂ꂽ");
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
