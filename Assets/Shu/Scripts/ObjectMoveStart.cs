using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveStart : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    private bool isMove = false;
    [SerializeField] private float speed;
    [SerializeField] private float rotate;
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
        if (collision.gameObject.tag == "Player")
        {
            if (!isMove)
            {
                foreach(var Object in objects)
                {
                    Object.GetComponent<Rigidbody2D>().gravityScale = 1;
                }
            }
        }
    }
}
