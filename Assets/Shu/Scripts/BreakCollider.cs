using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakCollider : MonoBehaviour
{
    private PlayerController playercontroller;
    private CameraController cameracontroller;
    [SerializeField] Tilemap tilemap;
    [Header("InspectorÇ≈ëÄçÏÇµÇ»Ç¢")]public int X;
    [Header("InspectorÇ≈ëÄçÏÇµÇ»Ç¢")] public int Y;

    // Start is called before the first frame update
    void Start()
    {
        playercontroller = FindObjectOfType<PlayerController>();
        cameracontroller = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveWoodBox()
    {

    }

    private void BreakWoodBox()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playercontroller.playerstate == PlayerController.PlayerState.Capsule)
            {
                if (Mathf.Abs(playercontroller.speed) >= playercontroller.CapsuleLimitSpeed * (playercontroller.BreakProportion / 100))
                {
                    //ñÿî†Ç™âÛÇÍÇÈ
                    tilemap.SetTile(new Vector3Int(X, Y, 0), null);
                    playercontroller.speed = playercontroller.speed * ((100 - playercontroller.HitBoxDeceleration) / 100);
                    cameracontroller.HitShake(0.25f, 0.05f);
                    Destroy(this.gameObject);
                }
                else
                {
                    //playercontroller.BounceBack();
                }
            }
        }
    }

}
