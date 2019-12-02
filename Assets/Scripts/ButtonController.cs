using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonConttroller : MonoBehaviour
{
    [Range(1, 4)]
    public int PlayerNo = 1;
    public int pushCnt = 0;
    public int pushMaxCnt = 0;
    public bool ButtonMode;
    private MoveTo move;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<MoveTo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("0"))
        {
            pushCnt++;
        }

        if(ButtonMode)
        {
            CheckPushCnt();
        }
        else
        {

        }
    }

    void CheckPushCnt()
    {
        if(pushCnt >= pushMaxCnt)
        {
            move.flag = true;
            pushCnt = 0;
        }
    }
}
