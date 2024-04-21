using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// SasakiShu
/// ボタンセレクト
/// </summary>
public class SelectManager : MonoBehaviour
{
    public GameObject[] SelectButtons = new GameObject[3];

    public enum SelectState
    {
        Stage1, Stage2, Stage3
    }
    SelectState selectstate;

    void Start()
    {
        selectstate = SelectState.Stage1;
    }

    void Update()
    {
        var rtf = GetComponent<RectTransform>();
        switch (selectstate)
        {
            case SelectState.Stage1:
                break;
            case SelectState.Stage2:
                break;
            case SelectState.Stage3:
                break;
        }
    }
}
