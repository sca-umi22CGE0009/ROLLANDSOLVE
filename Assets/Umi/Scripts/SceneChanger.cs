using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// u.sasak
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [SerializeField,Header("遷移時間(クリアシーンのみ適応)")] private float changeTime = 3.0f;

    public int stageNum = 1;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        stageNum = PlayerPrefs.GetInt("Stage", stageNum);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            SceneManager.LoadScene("GameOver");
        }
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            //stageNum = 1;
            PlayerPrefs.SetInt("Stage", 1);
            PlayerPrefs.Save();
        }
        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            //stageNum = 2;
            PlayerPrefs.SetInt("Stage", 2);
            PlayerPrefs.Save();
        }
        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            //stageNum = 3;
            PlayerPrefs.SetInt("Stage", 3);
            PlayerPrefs.Save();
        }

    }

    //リトライ
    public void RetryButton()
    {
        if (stageNum == 1)
        {
            SceneManager.LoadScene("Stage1");
        }
        if (stageNum == 2)
        {
            SceneManager.LoadScene("Stage2");
        }
        if (stageNum == 3)
        {
            SceneManager.LoadScene("Stage3");
        }
    }
    //Clearしたらデータを消す
    public void OnAnimEnd()
    {
        if (SceneManager.GetActiveScene().name == "Clear")
        {
            StartCoroutine(SceneChange());
            PlayerPrefs.DeleteAll();
        }
    }
    //TitleSceneに遷移
    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(changeTime);
        SceneManager.LoadScene("TitleScene");
    }
}
