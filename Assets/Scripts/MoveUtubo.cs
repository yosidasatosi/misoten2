using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUtubo : MonoBehaviour
{
    public float Wait;

    private UtuboManager utubomanager;
    private GameObject gameobject;
    private GameObject CamObj;

    private int MoveNo;

    private float[] Speed = new float[2];
    private float[] MoveTime = new float[2];
    private float Timing;
    private float Count;

    // 動作用変数
    private bool Move;

    // Start is called before the first frame update
    void Start()
    {
        // ゲームオブジェクトの探索
        gameobject = GameObject.Find("Utubo");

        // コンポーネント取得
        utubomanager = gameobject.GetComponent<UtuboManager>();

        // 動作用変数の初期化
        MoveNo = 0;

        // データの設定
        utubomanager.Set(ref Speed[0], ref Speed[1], ref MoveTime[0], ref MoveTime[1], ref Timing);

        // メインカメラコンポーネントの取得
        CamObj = Camera.main.gameObject;

        // 初期値設定
        Move = false;

        Speed[1] *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveNo < 2)
        {
            if (Move)
            {
                if (Count < MoveTime[MoveNo])
                {
                    // 前方向に移動
                    transform.Translate(0.0f, 0.0f, Speed[MoveNo] * Time.deltaTime, Space.Self);
                }
                else
                {
                    Move = false;
                    Count = 0.0f;
                    MoveNo++;
                }

                UpdateCount();
            }
            else
            {
                if (CamObj.transform.position.z + Timing > transform.position.z)
                {
                    if (MoveNo == 0)
                    {
                        // 動作開始
                        Move = true;
                    }
                    else
                    {
                        if (Count < Wait)
                        {
                            UpdateCount();
                        }
                        else
                        {
                            Count = 0.0f;
                            Move = true;
                        }
                    }
                }
            }
        }
    }

    void UpdateCount()
    {
        Count += Time.deltaTime;
    }
}
