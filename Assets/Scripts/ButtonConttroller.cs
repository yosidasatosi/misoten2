using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [Range(1, 4)]
    public int PlayerNo = 1;
    public int pushCnt = 0;
    public int pushMaxCnt = 0;
    public bool ButtonMode;
    private MoveTo move;
    private bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<MoveTo>();
        ButtonMode = false;
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            pushCnt++;
        }

        if(ButtonMode)
        {
            CheckPushCnt();
        }
        else
        {
            CheckPush();
        }
    }

    void CheckPushCnt()
    {
        if(pushCnt >= pushMaxCnt)
        {
            trigger = true;
        }
    }

    void CheckPush()
    {
        if (pushCnt > 0)
        {
            trigger = true;
        }
    }

    private void OnDisable()
    {
        if(trigger)
        {
            move.flag = true;
            move.moveOn = true;
            pushCnt = 0;
            trigger = false;
        }
    }
}
