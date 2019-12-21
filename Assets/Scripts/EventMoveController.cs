using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMoveController : MonoBehaviour
{
    // ギミック動作のデータのインナークラス
    [System.Serializable]
    public class MoveData
    {
        public Vector3[] startData;
        public Vector3[] endPosData;
        public Quaternion[] rotData;
        public Vector3 startPos;
        public Vector3 endPos;
    };

    // パターン関係データ
    public MoveData data;           // 動きのデータ
    public int   patternState;      // 現在のパターン番号
    public float[] changeTime;      // パターン変更時間
    public int[] movePattern;      // パターン格納
    private int nextState;          // 次のパターン番号
    private float startTime;        // パターン開始時間
    public float[] moveTime;         // 移動時間
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < movePattern.Length; i++)
        {
            movePattern[i] = i;
        }

        data.endPos = data.endPosData[0];
    }

    // Update is called once per frame
    void Update()
    {
        // 現在時間の取得
        float nowTime = Time.realtimeSinceStartup - startTime;
        Vector3 diff = data.endPos - transform.position;

        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
        }

        // パターンの変更チェック
        CheckPattern(nowTime);
        // 移動
        Move();
    }

    //=====================================================
    // ステート遷移関数
    //=====================================================
    void ChangeState()
    {
        // ステートの変更
        patternState = nextState;
        nextState++;

        // 位置の設定
        transform.SetPositionAndRotation(data.startData[patternState], data.rotData[patternState]);
        startTime = Time.realtimeSinceStartup;

        data.endPos = data.endPosData[patternState];
    }

    //====================================================
    // パターンの確認関数
    //====================================================
    void CheckPattern(float nowTime)
    {
        // 時間によるパターンの変更
        if ((int)nowTime == changeTime[patternState])
        {
            ChangeState();
        }
    }

    //=========================================================
    // 線形補間での移動関数
    //=========================================================
    void Move()
    {
        // 移動
        transform.position =
            Vector3.SmoothDamp(transform.position, data.endPos, ref velocity, moveTime[patternState]);
    }
}
