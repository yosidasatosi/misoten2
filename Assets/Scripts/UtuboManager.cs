using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtuboManager : MonoBehaviour
{
    public GameObject utuboPrefab;

    // 構造体を編集可能にする
    [System.Serializable]

    // パラメーター
    public struct Parameter
    {
        public float Start;
        public float Back;
    }

    // 構造体を編集可能にする
    [System.Serializable]

    // 初期値構造体
    public struct InitData
    {
        public GameObject utubomodel;
        public float Timing;
        public Parameter Speed;
        public Parameter MoveTime;
    }

    // 初期データ
    public List<InitData> InitDatas = new List<InitData>();

    // 参照用変数
    private int No;

    // 生成用変数
    private bool Pop;

    // Start is called before the first frame update
    void Start()
    {
        // 初期値設定
        No = 0;

        // 生成用フラグ
        Pop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Pop)
        {
            //Debug.Log(Time.deltaTime);
            for (int i = 0; i < InitDatas.Count; i++)
            {
                // プレファブからIwashiオブジェクトを生成
                GameObject Utubo = (GameObject)Instantiate(
                    utuboPrefab               // 生成するプレファブ設定
                    );

                // 初期位置設定
                Utubo.transform.localPosition = InitDatas[i].utubomodel.transform.localPosition;
                Utubo.transform.localRotation = InitDatas[i].utubomodel.transform.localRotation;
            }

            // 生成終了
            Pop = false;
        }
    }
    
    // 初期値データ数取得関数
    public int GetCount()
    {
        return InitDatas.Count;
    }

    // 動作用データの設定
    public void Set(ref float Sspd, ref float Bspd, ref float Stime, ref float Btime, ref float Timing)
    {
        // 移動速度
        Sspd = InitDatas[No].Speed.Start;
        Bspd = InitDatas[No].Speed.Back;

        // 動作時間
        Stime = InitDatas[No].MoveTime.Start;
        Btime = InitDatas[No].MoveTime.Back;

        // タイミング
        Timing = InitDatas[No].Timing;

        // 参照用変数更新
        No++;
    }
}
