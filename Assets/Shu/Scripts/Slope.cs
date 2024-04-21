using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slope : MonoBehaviour
{
    public float Angle;
    public float UnitV;
    private PlayerController playercontroller;
    // Start is called before the first frame update
    void Start()
    {
        playercontroller = FindObjectOfType<PlayerController>();
        UnitV = 1.0f / Mathf.Sqrt(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
