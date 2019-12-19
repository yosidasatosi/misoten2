using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveSubCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private float cycleTime = 10.0f;

    [SerializeField]
    private float MoveSpeed = 0.1f;

    [SerializeField]
    private float MoveTime = 2.0f;

    private CinemachineTrackedDolly dolly;
    private float pathPositionMax;
    private float pathPositionMin;
    private float count;

    private bool MoveStart;

    // Start is called before the first frame update
    void Start()
    {
        // バーチャルカメラがセットされていいなければ中止
        if (this.virtualCamera == null)
        {
            this.enabled = false;
            return;
        }

        // ドリーコンポーネントを取得できなければ中止
        this.dolly = this.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        if (this.dolly == null)
        {
            this.enabled = false;
            return;
        }

        // countの初期化
        count = 0.0f;

        MoveStart = false;

        // Positonの単位をトラック上のウェイポイント番号基準にするよう設定
        this.dolly.m_PositionUnits = CinemachinePathBase.PositionUnits.PathUnits;

        // ウェイポイントの最大番号・最小番号を取得
        this.pathPositionMax = this.dolly.m_Path.MaxPos;
        this.pathPositionMin = this.dolly.m_Path.MinPos;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    MoveStart = true;
        //}

        if (!MoveStart)
        {
            if (MoveTime > count)
            {
                count += Time.deltaTime;
            }
            else
            {
                MoveStart = true;
            }
        }
        else
        {
            this.dolly.m_PathPosition += MoveSpeed;
        }
    }
}
