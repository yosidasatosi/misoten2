using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    // パターンの列挙型
    private enum MOVE_PATTERN
    {
        PATTERN01,  // 右から左へ
        PATTERN02,  // 左から右へ
        PATTERN03,  // 前方から左後方へ
        PATTERN04,  // 前方から右後方へ
        PATTERN05,  // 後ろから追う
        PATTERN_MAX
    }

    // ギミック動作のデータのインナークラス
    [System.Serializable]
    public class MoveData
    {
        public Vector3   startData;
        public Vector3[] endPosData;
        public Vector3 startPos;
        public Vector3 endPos;
    };

    // オブジェクト
    public Camera mainCamera;
    // パターン関係データ
    public MoveData data;         // 動きのデータ
    public int patternState;     // 現在のパターン番号
    private int[] movePattern;    // パターン格納
    private int nextState;        // 次のパターン番号
    public bool flag;
    public bool moveOn;
    // 定数データ
    private Vector3 velocity = Vector3.zero;
    private const int patternMax = (int)MOVE_PATTERN.PATTERN_MAX;   // パターンの最大番号
    private float moveTime = 0.5f;
    private float interval = 7.0f;
    private float startTime;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        // インスタンスの生成
        movePattern = new int[patternMax];
        // インスタンスの初期化
        startTime = Time.realtimeSinceStartup;
        nextState = (int)MOVE_PATTERN.PATTERN02;
        flag = false;
        moveOn = false;

        for (int i = 0; i < movePattern.Length; i++)
        {
            movePattern[i] = i;
        }

        // 座標データの初期化
        data.startData = transform.localPosition;
        data.startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 現在時間の取得
        float nowTime = Time.realtimeSinceStartup - startTime;

        if (flag)
        {
            GetComponent<Collider>().enabled = false;
            // 終点の更新
            data.endPos = data.endPosData[patternState] + mainCamera.transform.position;
            data.endPos.z = data.endPosData[patternState].z + data.startData.z + mainCamera.transform.position.z;
            data.startPos.z = data.startData.z + mainCamera.transform.position.z;
            // 移動処理
            Move();
        }

        // 状態遷移時間の確認
        CheckPattern(nowTime);
    }

    //=====================================================
    // ステート遷移関数
    //=====================================================
    void ChangeState()
    {
        if (patternState != patternMax)
        {   // ステートの変更
            patternState = nextState;
            nextState++;
        }

        if(patternState == (int)MOVE_PATTERN.PATTERN05)
        {
            GetComponent<ButtonController>().ButtonMode = true;
            interval = 11.0f;
        }

        GetComponent<Collider>().enabled = true;
        startTime = Time.realtimeSinceStartup;
        flag = false;
    }

    //====================================================
    // パターンの確認関数
    //====================================================
    void CheckPattern(float nowTime)
    {
        if ((int)nowTime >= interval)
        {
            ChangeState();
        }
    }

    //=========================================================
    // 線形補間での移動関数
    //=========================================================
    void Move()
    {
        if (data.endPosData[patternState] == new Vector3(0.0f, 0.0f, 0.0f)) return;

        if (time > 2.5) moveOn = false;

        if (!moveOn)
        {
            // 移動
            transform.position =
                Vector3.SmoothDamp(transform.position, data.startPos, ref velocity, moveTime);
            time = 0;
        }
        else
        {
            // 移動
            transform.position =
                Vector3.SmoothDamp(transform.position, data.endPos, ref velocity, moveTime);
            time += Time.deltaTime;
        }

    }
}
