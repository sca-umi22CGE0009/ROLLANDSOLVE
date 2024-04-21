using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeCheck : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private PlayerController playercontroller;
    private Slope slope;
    // Start is called before the first frame update
    void Start()
    {
        playercontroller = FindObjectOfType<PlayerController>();
        slope = FindObjectOfType<Slope>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slope")
        {
            Debug.Log("Ç±Ç±ÇÕç‚");
            playercontroller.SlopeAngle = collision.gameObject.GetComponent<Slope>().Angle;
            playercontroller.SlopeAngleX = slope.UnitV;
            playercontroller.SlopeAngleY = slope.UnitV;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slope")
        {
            //Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 0.1f, Player.transform.position.z);
            playercontroller.SlopeAngleX = 1.0f;
            playercontroller.SlopeAngleY = 0.0f;
        }
    }
}
