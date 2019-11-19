using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUtubo : MonoBehaviour
{
    private UtuboManager utubomanager;
    private GameObject gameobject;
    private GameObject CamObj;

    private float a = 0;

    private Vector3 InitPosition;
    private float Speed;

    // 動作用変数
    private bool Move;

    // 動作タイミング変数
    public float Timing = 7.0f;

    // 定数定義
    //public const float MOVE_TIMING = 7.0f;

    //private class Speed
    //{
    //    float Start;
    //    float End;
    //}

    //private Speed speed = new Speed();

    // Start is called before the first frame update
    void Start()
    {
        // ゲームオブジェクトの探索
        gameobject = GameObject.Find("Utubo");

        // コンポーネント取得
        utubomanager = gameobject.GetComponent<UtuboManager>();

        // 初期位置を保存
        InitPosition = transform.localPosition;

        //Debug.Log(InitPosition);

        // 移動速度を保存
        Speed = utubomanager.GetSpeed();

        // メインカメラコンポーネントの取得
        CamObj = Camera.main.gameObject;

        // 初期値設定
        Move = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CamObj.transform.position);

        if (Move)
        {
            a = InitPosition.x - transform.position.x;
            if (a < 0)
            {
                a *= -1;
            }

            if (a < 3.0f)
            {
                // 前方向に移動
                transform.Translate(0.0f, 0.0f, Speed * Time.deltaTime, Space.Self);
            }
        }
        else
        {
            if (CamObj.transform.position.z + Timing > transform.position.z)
            {
                // 動作開始
                Move = true;

                //Debug.Log("pop true");
            }
        }
    }
}
