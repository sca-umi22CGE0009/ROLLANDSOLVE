using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonController : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite selectedSprite;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if(button != null)
        {
            button.onClick.AddListener(OnButtonClicked);
        }
    }

    void OnButtonClicked()
    {
        Image image = button.GetComponent<Image>();
        if(image != null)
        {
            image.sprite = selectedSprite;
        }
    }
}