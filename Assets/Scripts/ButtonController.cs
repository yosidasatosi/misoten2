using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [Range(1,4)]
    public int PlayerNo = 1;
    public GameObject buttonUI;
    public int pushCnt = 0;
    public int pushMaxCnt = 0;
    public bool ButtonMode;
    private bool cheatFlag;
    public bool trigger;

    // Start is called before the first frame update
    void Start()
    {
        ButtonMode = false;
        cheatFlag = false;
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("push" + PlayerNo) || Input.GetKey(KeyCode.Space))
        {
            pushCnt++;
        }

        // UIが表示されてない場合
        if (!buttonUI.activeSelf)
        {   
            // 連打成功の確認
            CheckPushCnt();
            // 事前連打対策
            CheckCheatButton();
        }
        else if(buttonUI.activeSelf)
        {
            if(!cheatFlag)
            {
                CheckPush();
            }
            else
            {
                pushCnt = 0;
            }
        }
    }

    // 連打数の確認関数
    void CheckPushCnt()
    {
        if (!ButtonMode) return;

        if (pushCnt >= pushMaxCnt)
        {
            trigger = true;
            GetComponent<MoveTo>().flag = true;
            GetComponent<MoveTo>().moveOn = true;
            GetComponent<StarEffect>().use = true;

            pushCnt = 0;
        }
    }

    // タイミングの確認関数
    void CheckPush()
    {
        if (ButtonMode) return;

        if (pushCnt > 0)
        {
            trigger = true;
            GetComponent<MoveTo>().flag = true;
            GetComponent<MoveTo>().moveOn = true;
            GetComponent<StarEffect>().use = true;
            pushCnt = 0;
        }
    }

    // UI未表示時の連打対策
    void CheckCheatButton()
    {
        // タイミングボタン時の連打対策
        if (!ButtonMode)
        {
            CheckButtonCnt();
        }
        else
        {
            // UI表示前連打対策
            if (pushCnt > 0)
            {
                cheatFlag = false;
                pushCnt = 0;
            }
        }
    }

    // タイミングボタンの事前入力無効関数
    void CheckButtonCnt()
    {
        if (pushCnt > 3)
        {
            cheatFlag = true;
        }
        else
        {
            cheatFlag = false;
            trigger = false;
        }
    }
}
