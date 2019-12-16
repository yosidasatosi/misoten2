using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkManager : MonoBehaviour
{
    // 構造体
    [System.Serializable]
    public struct InitData
    {
        public Vector3 position;    // 位置
        public Vector3 rotation;    // 回転
    }


    // public変数
    public GameObject SharkPrefab;

    public float PopTime = 1.0f;    // 生成時間

    public List<InitData> InitDatas = new List<InitData>(); // 初期データリスト


    // private変数
    // int型
    private int DataNo;  // データ番号

    // float型
    private float PopCount;     // 生成用経過時間

    // bool型
    private bool Pop;
    private bool Delete;

    // コンポーネント取得用
    private GameObject gameobject;
    private IwashiManager IwashiScr;

    // クローン用
    private GameObject Shark;

    // Start is called before the first frame update
    void Start()
    {
        // 変数初期化
        DataNo = 0;
        PopCount = 0.0f;
        Pop = false;
        Delete = false;

        // ゲームオブジェクトの探索
        gameobject = GameObject.Find("Iwashi");

        // IwashiManagerコンポーネントを取得
        IwashiScr = gameobject.GetComponent<IwashiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Delete)
        {
            if (IwashiScr.GetDelete())
            {
                if (!Pop)
                {
                    if (CheckTime(PopTime, PopCount))
                    {
                        // プレファブからクローンを生成
                        Shark = (GameObject)Instantiate(
                            SharkPrefab,                // 生成するプレファブ設定
                            Camera.main.transform       // 親設定
                            );

                        Pop = true;
                    }
                    else
                    {
                        PopCount = CountUp(PopCount);
                    }
                }
            }
        }
    }

    // データの設定
    public void SetShark(ref Vector3 pos, ref Vector3 rot)
    {
        if (DataNo < InitDatas.Count)
        {
            pos = InitDatas[DataNo].position;
            rot = InitDatas[DataNo].rotation;

            DataNo++;
        }
        else
        {
            DeleteClone();
        }
    }

    // 時間経過カウント
    public float CountUp(float count)
    {
        count += Time.deltaTime;

        return count;
    }

    // 時間経過の確認
    public bool CheckTime(float time, float count)
    {
        if (time < count)
        {
            return true;
        }

        return false;
    }

    // 生成したクローンを削除する
    private void DeleteClone()
    {
        GameObject.Destroy(Shark);

        Delete = true;
    }
}
