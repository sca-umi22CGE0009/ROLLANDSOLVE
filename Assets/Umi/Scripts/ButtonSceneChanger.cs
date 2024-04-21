using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// u.sasak
/// </summary>
public class ButtonSceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject poseWindow;
    [SerializeField] private GameObject selectWindow;
    [SerializeField] private GameObject operationWindow;
    [SerializeField] private GameObject titleWindow;

    [SerializeField,Header("escでポーズ画面起動")] private string comment;

    [SerializeField] private GameObject countObj;
    [SerializeField] private Image[] countImage;
    [SerializeField] private float countTime = 3.0f;

    float ReadyStartCount = 1.0f;
    int keyNum;
    // Start is called before the first frame update
    void Start()
    {
        poseWindow.gameObject.SetActive(false);
        selectWindow.gameObject.SetActive(false);
        operationWindow.gameObject.SetActive(false);
        titleWindow.gameObject.SetActive(false);
        keyNum = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            keyNum += 1;
            poseWindow.gameObject.SetActive(!poseWindow.activeSelf);
            selectWindow.gameObject.SetActive(false);
            operationWindow.gameObject.SetActive(false);
            titleWindow.gameObject.SetActive(false);
            countObj.SetActive(false);
        }
        if (keyNum >= 2)
        {
            poseWindow.gameObject.SetActive(false);
            selectWindow.gameObject.SetActive(false);
            operationWindow.gameObject.SetActive(false);
            titleWindow.gameObject.SetActive(false);
            keyNum = 0;
        }
        countTime -= Time.unscaledDeltaTime;

        if (keyNum == 1 || countTime > 0f - 0.01f)
        {
            Time.timeScale = 0f;
            countDownImage();
        }
        else
        {
            Time.timeScale = 1.0f;
            for (int i = 0; i < countImage.Length - 1; i++)
            {
                countImage[i].gameObject.SetActive(false);
            }
            
            StartCoroutine(StartCount());

            countTime = -1;
        }
    }
    //カウントダウン
    private void countDownImage()
    {
        if (countTime >= 2)
        {
            countImage[0].gameObject.SetActive(true);
        }
        if (countTime < 2 && countTime >= 1)
        {
            countImage[0].gameObject.SetActive(false);
            countImage[1].gameObject.SetActive(true);
        }
        if (countTime < 1 && countTime >= 0)
        {
            countImage[1].gameObject.SetActive(false);
            countImage[2].gameObject.SetActive(true);
        }
        if (countTime < 0)
        {
            countImage[2].gameObject.SetActive(false);
        }
    }
    //private IEnumerator CountDown()
    //{
    //    //あとで直す
    //    countImage[0].gameObject.SetActive(true);
    //    yield return new WaitForSecondsRealtime(time);
    //    countImage[0].gameObject.SetActive(false);
    //    countImage[1].gameObject.SetActive(true);
    //    yield return new WaitForSecondsRealtime(time);
    //    countImage[1].gameObject.SetActive(false);
    //    countImage[2].gameObject.SetActive(true);
    //    yield return new WaitForSecondsRealtime(time);
    //    countImage[2].gameObject.SetActive(false);
    //    yield return new WaitForSecondsRealtime(time);

    //    //for (int i = 0; i < countImage.Length - 1; i++)
    //    //{
    //    //    countImage[i].gameObject.SetActive(true);
    //    //    yield return new WaitForSecondsRealtime(time);
    //    //    countImage[i].gameObject.SetActive(false);
    //    //    Debug.Log(i);
    //    //}
    //}
    //カウントダウンが0になったら
    private IEnumerator StartCount()
    {
        countImage[3].gameObject.SetActive(true);

        yield return new WaitForSeconds(ReadyStartCount);
        countObj.SetActive(false);
    }

    public void OnTitleButton()
    {
        titleWindow.SetActive(true);
        poseWindow.gameObject.SetActive(false);
    }
    public void TitleSceneButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void OnSelectButton()
    {
        poseWindow.gameObject.SetActive(false);
        selectWindow.gameObject.SetActive(true);
    }
    public void OnYesButton()
    {
        SceneManager.LoadScene("SelectScene");
    }
    public void OnNoButton()
    {
        selectWindow.gameObject.SetActive(false);
        titleWindow.SetActive(false);
        poseWindow.gameObject.SetActive(true);
    }
    public void OnOperationButton()
    {
        operationWindow.gameObject.SetActive(true);
        poseWindow.gameObject.SetActive(false);
    }
    public void OnReturnButton()
    {
        operationWindow.gameObject.SetActive(false);
        poseWindow.gameObject.SetActive(true);
    }
    public void OnStartButton()
    {
        keyNum = 0;
        poseWindow.gameObject.SetActive(false);
    }
}
