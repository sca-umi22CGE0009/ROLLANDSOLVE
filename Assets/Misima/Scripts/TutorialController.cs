using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Toggle dontShowAgainToggle;

    // Start is called before the first frame update
    void Start()
    {
        bool shoudShowTutorial = PlayerPrefs.GetInt("ShowTutorial",1) == 1;
        gameObject.SetActive(shoudShowTutorial);
        dontShowAgainToggle.isOn = !shoudShowTutorial; 
    }

    public void OnToggleValueChanged()
    {
        PlayerPrefs.SetInt("ShowTutorial",dontShowAgainToggle.isOn ? 0:1);
    }
}
