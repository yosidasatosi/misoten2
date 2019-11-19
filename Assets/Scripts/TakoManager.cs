using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakoManager : MonoBehaviour
{
    public List<GameObject> takoPrefab = new List<GameObject>();

    // 構造体を編集可能にする
    [System.Serializable]

    // 構造体
    public class InitData
    {
        public GameObject takomodel;
        public int MoveMode;
        public float Limit;
    }

    public float Speed;

    public List<InitData> InitDatas = new List<InitData>();

    private int no = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
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
        no++;
        no %= InitDatas.Count;

        return InitDatas[no].MoveMode;
    }

    // 移動限界取得関数
    public float GetLimit()
    {
        return InitDatas[no].Limit;
    }
}
