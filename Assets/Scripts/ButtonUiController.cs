using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//=========================================================
// ボタンUIの管理クラス
//=========================================================
public class ButtonUiController : MonoBehaviour
{
    private enum BUTTON_MODE
    {
        MODE01,
        MODE02,
        MODE_MAX
    }

    public GameObject Button;
    public GameObject[] inputOffPlayer;
    public int[] start;
    public int[] end;
    public int[] interval;
    public int[]   patternCnt;
    private int     pattern;
    private int drawCnt;
    private float startTime;
    private int changeTime;

    // Start is called before the first frame update
    void Start()
    {
        drawCnt = 0;
        startTime = Time.realtimeSinceStartup;
        changeTime = end[0] + interval[0];
        Button.SetActive(false);
        inputOffPlayer[0].GetComponent<ButtonController>().enabled = false;
        inputOffPlayer[1].GetComponent<ButtonController>().enabled = false;
        inputOffPlayer[2].GetComponent<ButtonController>().enabled = false;
        inputOffPlayer[3].GetComponent<ButtonController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 現在のパターンの経過時間
        float nowTime = Time.realtimeSinceStartup - startTime;

        UIManagerByTime(nowTime);
        CheckCnt();
    }

    // パターンの変更
    void CheckCnt()
    {
        if (drawCnt >= patternCnt[pattern])
        {
            pattern++;
            drawCnt = 0;
            startTime = Time.realtimeSinceStartup;
            changeTime = end[pattern] + interval[pattern];
        }
    }

    // 時間制御でのUI描画
    void UIManagerByTime(float nowTime)
    {
        // 時間によるパターンの変更
        if ((int)nowTime >= changeTime)
        {
            drawCnt++;
            startTime = Time.realtimeSinceStartup;
            inputOffPlayer[0].GetComponent<MoveTo>().flag = false;
            inputOffPlayer[1].GetComponent<MoveTo>().flag = false;
            inputOffPlayer[2].GetComponent<MoveTo>().flag = false;
            inputOffPlayer[3].GetComponent<MoveTo>().flag = false;
        }
        else if ((int)nowTime >= end[pattern])
        {
            Button.SetActive(false);
            inputOffPlayer[0].GetComponent<ButtonController>().enabled = false;
            inputOffPlayer[1].GetComponent<ButtonController>().enabled = false;
            inputOffPlayer[2].GetComponent<ButtonController>().enabled = false;
            inputOffPlayer[3].GetComponent<ButtonController>().enabled = false;
        }
        else if ((int)nowTime >= start[pattern])
        {
            Button.SetActive(true);
            inputOffPlayer[0].GetComponent<ButtonController>().enabled = true;
            inputOffPlayer[1].GetComponent<ButtonController>().enabled = true;
            inputOffPlayer[2].GetComponent<ButtonController>().enabled = true;
            inputOffPlayer[3].GetComponent<ButtonController>().enabled = true;
        }
    }

    public int GetDrwaCnt()
    {
        return drawCnt;
    }
}
