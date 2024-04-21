using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WoodBoxController : MonoBehaviour
{
    [SerializeField] private GameObject WoodBox;
    [SerializeField] private Rigidbody2D rb2d;
    private PlayerController playercontroller;
    private HumanMotion humanMotion;
    private HitRightCheck hitRightCheck;
    private HitLeftCheck hitLeftCheck;
    [SerializeField] private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        playercontroller = FindObjectOfType<PlayerController>();
        humanMotion = FindObjectOfType<HumanMotion>();
        hitRightCheck = FindObjectOfType<HitRightCheck>();
        hitLeftCheck = FindObjectOfType<HitLeftCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            //WoodBox.tag = "Stage";
            if (playercontroller.hitWoodBoxRight)
            {
                hitRightCheck.PlayerStop();
                playercontroller.hitWoodBoxRight = false;
            }
            else if (playercontroller.hitWoodBoxLeft)
            {
                hitLeftCheck.PlayerStop();
                playercontroller.hitWoodBoxLeft = false;
            }
            rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            rb2d.freezeRotation = true;
            //playercontroller.hitWoodBoxRight = false;
            //playercontroller.hitWoodBoxLeft = false;
            //humanMotion.HumanStop();
            //playercontroller.speed = 0.0f;
        }
    }
}
