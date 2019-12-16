using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSharkAsase : MonoBehaviour
{
    // 構造体
    [System.Serializable]
    public struct SharkData
    {
        public Vector3 position;    // 位置
        public Vector3 rotation;    // 回転
    }

    // public変数
    public float MoveTime = 4.0f;
    public float Speed = 10.0f;

    // private変数
    private int MoveNo;
    private int DataNo;

    private float MoveCount;

    private SharkData Shark = new SharkData();

    private GameObject gameobject;
    private SharkManager SharkScr;

    // Start is called before the first frame update
    private void Start()
    {
        // ゲームオブジェクトの探索
        gameobject = GameObject.Find("Shark");

        // SharkManagerコンポーネントを取得
        SharkScr = gameobject.GetComponent<SharkManager>();

        SetData();
    }

    // Update is called once per frame
    void Update()
    {
        if (SharkScr.CheckTime(MoveTime, MoveCount))
        {
            MoveNo++;
            MoveCount = 0.0f;

            SetData();
        }
        else
        {
            transform.Translate(0.0f, 0.0f, Speed * Time.deltaTime, Space.Self);

            MoveCount = SharkScr.CountUp(MoveCount);
        }
    }

    // データの格納と設定
    void SetData()
    {
        SharkScr.SetShark(ref Shark.position, ref Shark.rotation);

        transform.localPosition = Shark.position;
        transform.localRotation = Quaternion.Euler(Shark.rotation);
    }
}
