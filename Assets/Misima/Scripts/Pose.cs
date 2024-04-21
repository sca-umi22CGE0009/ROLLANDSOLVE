using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(resume);
    }

    private void Pause() 
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    private void resume() 
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
