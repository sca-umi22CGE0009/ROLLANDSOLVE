using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStage : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private bool onFloor = false;
    private BlockMove blockmove;
    // Start is called before the first frame update
    void Start()
    {
        blockmove = FindObjectOfType<BlockMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onFloor)
        {
            Player.transform.position += new Vector3(0, blockmove.moveSpeed, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onFloor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onFloor = false;
        }
    }
}
