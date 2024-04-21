using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    public GameObject initialImageObject;
    public GameObject newImageObject;

    // Start is called before the first frame update
    void Start()
    {
        initialImageObject.SetActive(true);
        newImageObject.SetActive(false);
    }
    public void ClearStage()
    {
        initialImageObject.SetActive(false);

        newImageObject.SetActive(true);
    }
}
