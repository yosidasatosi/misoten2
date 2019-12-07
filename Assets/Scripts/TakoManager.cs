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
        public float StopPosition;      // 停止位置
        public float PopTime;           // クローン生成時間
    }

    // float型変数
    //public float PopTime;   // クローン生成時間
    public float Speed;     // 移動速度

    // List型
    public List<InitData> InitDatas = new List<InitData>();

    // private
    // int型変数
    private int no;         // データ設定用変数
    private int PopNo;      // 生成用変数

    // float型変数
    private float count;    // 経過時間

    // bool型変数
    private bool Pop;       // 生成フラグ

    // Start is called before the first frame update
    void Start()
    {
        // private変数初期化
        no = 0;
        PopNo = 0;

        count = 0.0f;

        Pop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Pop)
        {
            if (count > InitDatas[PopNo].PopTime)
            {
                Debug.Log(PopNo);

                for (int i = 0; i < takoPrefab.Count; i++)
                {
                    // プレファブからTakoオブジェクトを生成
                    GameObject Tako = (GameObject)Instantiate(
                        takoPrefab[i],              // 生成するプレファブ設定
                        Camera.main.transform       // 親設定
                        );

                    // 初期位置設定
                    Tako.transform.localPosition = InitDatas[PopNo].takomodel.transform.localPosition;
                    Tako.transform.localRotation = InitDatas[PopNo].takomodel.transform.localRotation;
                }

                PopNo++;
            }
            else
            {
                count += Time.deltaTime;
            }

            if (PopNo >= InitDatas.Count)
            {
                Pop = false;
            }
        }
    }

    // 各種パラメータ設定関数
    public void SetTakoPar(bool Main, ref float Spd, ref float StopPos)
    {
        Spd = Speed;
        StopPos = InitDatas[no].StopPosition;

        if (!Main)
        {
            no++;
        }
    }
}
