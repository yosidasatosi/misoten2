using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//=========================================================
// ボタンUIの管理クラス
//=========================================================
public class ButtonUiController : MonoBehaviour
{
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
            if (pattern != (patternCnt.Length -1))
            {
                pattern++;
                drawCnt = 0;
                startTime = Time.realtimeSinceStartup;
                changeTime = end[pattern] + interval[pattern];
            }
            else
            {
                this.enabled = false;
            }
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
        }
        else if ((int)nowTime >= end[pattern])
        {
            Button.SetActive(false);
        }
        else if ((int)nowTime >= start[pattern])
        {
            Button.SetActive(true);
        }
    }
}
