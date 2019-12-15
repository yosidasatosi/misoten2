using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{

    // モデル
    private enum MODEL
    {
        MODEL01,
        MODEL02,
        MODEL_MAX
    }

    // パターンの列挙型
    private enum MOVE_PATTERN
    {
        PATTERN01,  // 右から左へ
        PATTERN02,  // 左から右へ
        PATTERN03,  // 前方から左後方へ
        PATTERN04,  // 前方から右後方へ
        PATTERN05,  // 後ろから追う
        PATTERN06,
        PATTERN07,
        PATTERN_MAX
    }

    [System.Serializable]
    // ギミック動作のデータのインナークラス
    public class MoveData
    {
        public Vector3[] startData;
        public Vector3[] endPosData;
        public Quaternion[] rotData;
        public Vector3 startPos;
        public Vector3 endPos;
    };

    // オブジェクト
    public GameObject[] shark;
    public GameObject button;
    public Camera mainCamera;
    private int modelState;
    // パターン関係データ
    public MoveData data;         // 動きのデータ
    public int patternState;     // 現在のパターン番号
    private int[] movePattern;    // パターン格納
    private int nextState;        // 次のパターン番号
    public float slowStartTime;   // スローモーションの開始時間
    private Animator anim;
    private int atcTime;
    private bool atcFlag;
    private bool changeState;
    public int changeMove;          // moveの関数変更開始パターン
    private int atcCnt;
    // 定数データ
    private Vector3 velocity = Vector3.zero;
    private const int patternMax = (int)MOVE_PATTERN.PATTERN_MAX;   // パターンの最大番号
    private float moveTime = 3.0f;
    private float interval = 7.0f;
    private const float changeTime = 1.0f;
    private float startTime;
    private const int modelMax = (int)MODEL.MODEL_MAX;

    // Start is called before the first frame update
    void Start()
    {
        // インスタンスの生成
        movePattern = new int[patternMax];
        data = new MoveData();
        data.startData = new Vector3[patternMax];
        data.endPosData = new Vector3[patternMax];
        data.rotData = new Quaternion[patternMax];
        anim = shark[1].GetComponent<Animator>();
        // インスタンスの初期化
        modelState = (int)MODEL.MODEL01;
        shark[(int)MODEL.MODEL02].SetActive(false);
        startTime = Time.realtimeSinceStartup;
        nextState = (int)MOVE_PATTERN.PATTERN02;
        slowStartTime = 2.0f;
        changeState = false;

        for(int i = 0; i < movePattern.Length; i++)
        {
            movePattern[i] = i;
        }

        // 座標・回転データの初期化
        data.startData[(int)MOVE_PATTERN.PATTERN01] = new Vector3(210.0f, 0.0f, 20.0f);
        data.startData[(int)MOVE_PATTERN.PATTERN02] = new Vector3(-180.0f, 0.0f, 20.0f);
        data.startData[(int)MOVE_PATTERN.PATTERN03] = new Vector3(50.0f, 0.0f, 170.0f);
        data.startData[(int)MOVE_PATTERN.PATTERN04] = new Vector3(-50.0f, 0.0f, 170.0f);
        data.startData[(int)MOVE_PATTERN.PATTERN05] = new Vector3(0.0f, 50.0f, -80.0f);

        data.endPosData[(int)MOVE_PATTERN.PATTERN01] = new Vector3(-170.0f, 0.0f, 20.0f);
        data.endPosData[(int)MOVE_PATTERN.PATTERN02] = new Vector3(150.0f, 0.0f, 20.0f);
        data.endPosData[(int)MOVE_PATTERN.PATTERN03] = new Vector3(-70.0f, 00.0f, -80.0f);
        data.endPosData[(int)MOVE_PATTERN.PATTERN04] = new Vector3(70.0f, 0.0f, -80.0f);
        data.endPosData[(int)MOVE_PATTERN.PATTERN05] = new Vector3(0.0f, 0.0f, -25.0f);
        data.endPosData[(int)MOVE_PATTERN.PATTERN06] = new Vector3(0.0f, 0.0f, -5.0f);
        data.endPosData[(int)MOVE_PATTERN.PATTERN07] = new Vector3(0.0f, 0.0f, -300.0f);

        data.rotData[(int)MOVE_PATTERN.PATTERN01] = Quaternion.Euler(0.0f, 270.0f, 0.0f);
        data.rotData[(int)MOVE_PATTERN.PATTERN02] = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        data.rotData[(int)MOVE_PATTERN.PATTERN03] = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        data.rotData[(int)MOVE_PATTERN.PATTERN04] = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        data.rotData[(int)MOVE_PATTERN.PATTERN05] = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        data.startPos = data.endPosData[(int)MOVE_PATTERN.PATTERN05];

        // サメの初期位置と方向
        shark[modelState].transform.SetPositionAndRotation(mainCamera.transform.position + data.startData[patternState], 
                                                                                data.rotData[patternState]);
    }

    // Update is called once per frame
    void Update()
    {
        // 現在時間の取得
        float nowTime = Time.realtimeSinceStartup - startTime;
        // 終点の更新
        data.endPos = data.endPosData[patternState] + mainCamera.transform.position;

        if (!changeState)
        {
            if (patternState > (int)MOVE_PATTERN.PATTERN02)
            {
                if ((int)nowTime == 0.0f)
                {
                    anim.Rebind();
                }
                if ((int)nowTime == 1.0f)
                {
                    anim.Play("CINEMA_4D___ 0", 0, 0.0f);
                }
            }
            // 移動処理
            FirstMove();
        }
        else if (changeState)
        {
            // ボタンが非表示になった時に動作開始
            if (button.activeSelf == false)
            {
                data.startPos = data.endPosData[(int)MOVE_PATTERN.PATTERN05] + mainCamera.transform.position;
                SecondMove();
            }
            else
            {
                atcTime = 0;
            }
        }

        // 状態遷移時間の確認
        CheckPattern(nowTime);
    }

    //=====================================================
    // モデルの変更関数
    //=====================================================
    void ChangeModel()
    {
        // パターン３からモデル変更
        if(patternState == (int)MOVE_PATTERN.PATTERN03)
        {
            shark[modelState].SetActive(false);
            modelState++;
            shark[modelState].SetActive(true);
        }
    }

    //=====================================================
    // ステート遷移関数
    //=====================================================
    void ChangeState()
    {
        if (patternState < (int)MOVE_PATTERN.PATTERN05)
        {   // ステートの変更
            patternState = nextState;
            nextState++;

            // モデルの変更
            ChangeModel();

            // 位置の設定
            shark[modelState].transform.SetPositionAndRotation(mainCamera.transform.position + data.startData[patternState],
                                                                                    data.rotData[patternState]);
        }


        if(patternState == (int)MOVE_PATTERN.PATTERN06)
        {
            atcCnt++;

            if (atcCnt >= 3)
            {
                patternState = (int)MOVE_PATTERN.PATTERN07;
                changeState = false;
                moveTime = 5.0f;
                atcCnt = 0;
                anim.speed = 0.7f;

                return;
            }
        }

        if (patternState == changeMove)
        {
            patternState = nextState;
            changeState = true;
            moveTime = 1.0f;
            interval = 11.0f;
        }

        startTime = Time.realtimeSinceStartup;
    }

    //====================================================
    // パターンの確認関数
    //====================================================
    void CheckPattern(float nowTime)
    {
        // パターン４までスローモーション判定
        if (patternState <= (int)MOVE_PATTERN.PATTERN04)
        {   
            // スローモーションのチェック
            CheckSlowTime(nowTime);
        }

        // 時間によるパターンの変更
        if ((int)nowTime == interval)
        {
            ChangeState();
        }
    }

    //=========================================================
    // 線形補間での移動関数
    //=========================================================
    void FirstMove()
    {
        // 移動
        shark[modelState].transform.position =
            Vector3.SmoothDamp(shark[modelState].transform.position, data.endPos, ref velocity, moveTime);
    }

    //=========================================================
    // 線形補間での移動関数
    //=========================================================
    void SecondMove()
    {
        if (atcTime > 90)
        {
            // 移動
            shark[modelState].transform.position =
                Vector3.SmoothDamp(shark[modelState].transform.position, data.startPos, ref velocity, moveTime);
        }
        else
        {
            // 移動
            shark[modelState].transform.position =
                Vector3.SmoothDamp(shark[modelState].transform.position, data.endPos, ref velocity, moveTime);
            atcTime++;
        }
    }

    //===============================================
    // スローモーションの切替関数
    //===============================================
    void CheckSlowTime(float nowTime)
    {
        // スローモーション
        if (button.activeSelf == true)
        {
            Time.timeScale = 0.1f;
        }
        else if (button.activeSelf == false)
        {
            Time.timeScale = 1.0f;
        }
    }

}
