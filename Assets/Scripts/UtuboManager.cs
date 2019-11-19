using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtuboManager : MonoBehaviour
{
    public GameObject utuboPrefab;

    // 構造体を編集可能にする
    [System.Serializable]

    // 構造体
    public class InitData
    {
        public GameObject utubomodel;
        public float StartSpeed;
        //public int StartSpeed;
        //public int EndSpeed;
    }

    //public class Speed
    //{
    //    public int Start;
    //    public int End;
    //}

    public List<InitData> InitDatas = new List<InitData>();
    //public Speed speed = new Speed();

    private int no;

    // 生成用変数
    private bool pop;

    // Start is called before the first frame update
    void Start()
    {
        // 初期値設定
        no = -1;

        pop = true;

        //Debug.Log("ManagerStart");
    }

    // Update is called once per frame
    void Update()
    {
        if(pop)
        {
            //Debug.Log(Time.deltaTime);
            for (int i = 0; i < InitDatas.Count; i++)
            {
                //Debug.Log("ManagerUpdate");

                // プレファブからIwashiオブジェクトを生成
                GameObject Utubo = (GameObject)Instantiate(
                    utuboPrefab               // 生成するプレファブ設定
                    );

                // 初期位置設定
                Utubo.transform.localPosition = InitDatas[i].utubomodel.transform.localPosition;
                Utubo.transform.localRotation = InitDatas[i].utubomodel.transform.localRotation;
            }

            // 生成終了
            pop = false;
        }
    }

    // 移動速度取得関数
    public float GetSpeed()
    {
        no++;
        no %= InitDatas.Count;

        return InitDatas[no].StartSpeed;
    }

    //public Speed GetSpeed()
    //{
    //    return speed;
    //}

    // 初期値データ数取得関数
    public int GetCount()
    {
        return InitDatas.Count;
    }
}
