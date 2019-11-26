using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakoManager : MonoBehaviour
{
    // public
    // クローンするモデル
    public List<GameObject> takoPrefab = new List<GameObject>();

    // 構造体を編集可能にする
    [System.Serializable]

    // 構造体
    public class InitData
    {
        public GameObject takomodel;    // 参照するモデル
        public int MoveMode;            // 動作方法
        public float StopPosition;      // 停止位置
    }

    // float型変数
    public float PopTime;   // クローン生成時間
    public float Speed;     // 移動速度

    // List型
    public List<InitData> InitDatas = new List<InitData>();

    // private
    // int型変数
    private int no;         // データ設定用変数

    // float型変数
    private float count;    // 経過時間

    // bool型変数
    private bool Pop;       // 生成フラグ

    // Start is called before the first frame update
    void Start()
    {
        // private変数初期化
        no = 0;

        count = 0.0f;

        Pop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Pop)
        {
            if (count > PopTime)
            {
                for (int i = 0; i < takoPrefab.Count; i++)
                {
                    for (int j = 0; j < InitDatas.Count; j++)
                    {
                        // プレファブからTakoオブジェクトを生成
                        GameObject Tako = (GameObject)Instantiate(
                            takoPrefab[i],            // 生成するプレファブ設定
                            Camera.main.transform     // 親設定
                            );

                        // 初期位置設定
                        Tako.transform.localPosition = InitDatas[j].takomodel.transform.localPosition;
                        Tako.transform.localRotation = InitDatas[j].takomodel.transform.localRotation;
                    }
                }

                Pop = false;
            }
        }

        if (count < PopTime)
        {
            count += Time.deltaTime;
        }
    }

    // 移動速度取得関数
    public float GetSpeed()
    {
        return Speed;
    }

    // 動作方法取得関数
    public int GetMoveMode()
    {
        //no++;
        no %= InitDatas.Count;

        return InitDatas[no].MoveMode;
    }

    // 移動限界取得関数
    public float GetStopPos()
    {
        return InitDatas[no++].StopPosition;
    }
}
