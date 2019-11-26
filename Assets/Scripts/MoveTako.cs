using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTako : MonoBehaviour
{
    // TakoManagerスクリプト用変数
    private TakoManager takomanager;
    private GameObject gameobject;

    // 墨のパーティクル用変数
    private ParticleSystem SumiParticle;

    // int型
    private int MoveMode;       // 動作モード

    // float型
    private float Speed;        // 移動速度
    private float StopPos;      // 停止位置
    private float FireTime;     // 墨発射時間
    private float FireCount;    // 墨発射経過秒数

    // bool型
    private bool Fire;          // 墨発射フラグ
    private bool Stop;          // 停止フラグ
    private bool ReStart;       // 再動作フラグ

    // Start is called before the first frame update
    void Start()
    {
        // ゲームオブジェクトの探索
        gameobject = GameObject.Find("Tako");

        // コンポーネント取得
        takomanager = gameobject.GetComponent<TakoManager>();

        // 移動速度を保存
        Speed = takomanager.GetSpeed();

        // 動作方法の保存
        MoveMode = takomanager.GetMoveMode();

        // 停止位置の保存
        StopPos = takomanager.GetStopPos();

        // 墨を吐いている時間設定
        FireTime = 5.0f;

        // 墨を吐き始めてからの時間経過
        FireCount = 0.0f;

        // 墨のパーティクルコンポーネントの取得
        SumiParticle = this.GetComponent<ParticleSystem>();

        // パーティクルの動作を停止
        SumiParticle.Stop();

        // bool初期化
        Fire = false;
        Stop = false;
        ReStart = false;

        // 移動方向設定
        SetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ReStart)
        { // ReStart = false
            CheckStop();    // 停止するかどうか
        }

        if (Stop)
        { // Stop = true
            if (!Fire)
            { // Fire = false
                //墨を吐く処理
                SumiParticle.Play();

                // 墨発射開始
                Fire = true;
            }
            else
            { // Fire = true
                if (FireTime > FireCount)
                { // 発射時間が発射経過時間より大きい
                    FireCount += Time.deltaTime;
                }
                else
                { // 発車時間が発射経過時間より小さい
                    Fire = false;   // 墨発射終了
                    Stop = false;   // 停止終了
                    ReStart = true; // 再動作開始
                }
            }
        }
        else
        { // Stop = false
            // 対応した方向に移動
            transform.Translate(0.0f, Speed * Time.deltaTime, 0.0f, Space.Self);
        }
    }

    // 停止判定関数
    void CheckStop()
    {
        switch (MoveMode)
        {
            case 0: // 下方向に移動
                if (transform.localPosition.y < StopPos)
                { // 停止位置より小さくなった時
                    Stop = true;    // 停止開始
                }
                break;
            case 1: // 上方向に移動
                if (transform.localPosition.y > StopPos)
                { // 停止位置より大きくなった時
                    Stop = true;    // 停止開始
                }
                break;
        }
    }

    // 移動方向設定関数
    void SetSpeed()
    {
        if (MoveMode == 0)
        {
            Speed *= -1;
        }
    }
}
