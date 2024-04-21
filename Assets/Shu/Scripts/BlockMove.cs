using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockMove : MonoBehaviour
{
    [SerializeField] GameObject MoveStage;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tile tile;
    [SerializeField] float limitDownY;
    [SerializeField] float limitUpY;
    [SerializeField] int limitDownTileY;
    [SerializeField] int limitUpTileY;
    [SerializeField] int xMin;
    [SerializeField] int xMax;
    private BlueButtonPush blueButtonPush;
    public enum MoveState { Up, Down}
    private MoveState movestate;
    public float moveSpeed;
    private bool isLimit = false;
    // Start is called before the first frame update
    void Start()
    {
        blueButtonPush = GetComponent<BlueButtonPush>();
        movestate = MoveState.Down;
        moveSpeed = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (blueButtonPush.WoodBoxIsPush || blueButtonPush.isPush)
        {
            if (movestate == MoveState.Up)
            {
                MoveUp();
            }
            else if (movestate == MoveState.Down)
            {
                MoveDown();
            }
        }
    }

    private void MoveUp()
    {
        if (MoveStage.transform.position.y > limitUpY)
        {
            MoveStage.transform.position = new Vector3(MoveStage.transform.position.x, limitUpY, 0);
            MoveStage.SetActive(false);
            for (int x = xMin; x < xMax + 1; ++x)
            {
                tilemap.SetTile(new Vector3Int(x, limitUpTileY, 0), tile);
            }
            if (!isLimit)
            {
                isLimit = true;
                StartCoroutine(Stay());
            }
        }
        else
        {
            MoveStage.transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        }
    }

    private void MoveDown()
    {
        if (MoveStage.transform.position.y < limitDownY)
        {
            MoveStage.transform.position = new Vector3(MoveStage.transform.position.x, limitDownY, 0);
            MoveStage.SetActive(false);
            for (int x = xMin; x < xMax + 1; ++x)
            {
                tilemap.SetTile(new Vector3Int(x, limitDownTileY, 0), tile);
            }
            if (!isLimit)
            {
                isLimit = true;
                StartCoroutine(Stay());
            }
        }
        else
        {
            MoveStage.transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        }
    }

    private IEnumerator Stay()
    {
        yield return new WaitForSeconds(4.0f);
        if (movestate == MoveState.Up)
        {
            for (int x = xMin; x < xMax + 1; ++x)
            {
                tilemap.SetTile(new Vector3Int(x, limitUpTileY, 0), null);
            }
            MoveStage.SetActive(true);
            moveSpeed = -1.0f;
            movestate = MoveState.Down;
            isLimit = false;
        }
        else if(movestate == MoveState.Down)
        {
            for (int x = xMin; x < xMax + 1; ++x)
            {
                tilemap.SetTile(new Vector3Int(x, limitDownTileY, 0), null);
            }
            MoveStage.SetActive(true);
            moveSpeed = 1.0f;
            movestate = MoveState.Up;
            isLimit = false;
        }
        yield return null;
    }
}
