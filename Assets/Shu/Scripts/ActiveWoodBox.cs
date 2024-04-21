using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ActiveWoodBox : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tile tile;
    [SerializeField] public int X;
    [SerializeField] public int Y;
    [SerializeField] GameObject ButtonCheck;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WoodBox")
        {
            Debug.Log("–Ø” ‚ª“–‚½‚Á‚½");
            GameObject breakcollider = collision.transform.GetChild(0).gameObject;
            breakcollider.GetComponent<BreakCollider>().X = X;
            breakcollider.GetComponent<BreakCollider>().Y = Y;
            collision.transform.DetachChildren();
            if (ButtonCheck != null)
            {
            }
            if (ButtonCheck.GetComponent<BlueButtonPush>().TileInstantiate)
            {
                ButtonCheck.GetComponent<BlueButtonPush>().ActiveTile();
                ButtonCheck.GetComponent<BlueButtonPush>().WoodBoxIsPush = true;
            }
            else
            {
                ButtonCheck.GetComponent<BlueButtonPush>().InactiveTile();
                ButtonCheck.GetComponent<BlueButtonPush>().WoodBoxIsPush = true;
            }
            tilemap.SetTile(new Vector3Int(X, Y, 0), tile);
            Destroy(collision.gameObject);
        }
    }
}
