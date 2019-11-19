using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIwashi : MonoBehaviour
{
    public float Speed = 10.0f;
    public float TurningSpeed = 1.0f;
    public float TimeLimit = 5.0f;

    private GameObject gameobject;

    private IwashiManager iwashimanager;

    private Quaternion TarRot;

    private bool rotate;
    private bool change;
    private bool Parent;

    private float count;

    // Start is called before the first frame update
    void Start()
    {
        // ゲームオブジェクトの探索
        gameobject = GameObject.Find("Iwashi");

        // IwashiManagerのSetIwashiメソッドを使用
        iwashimanager = gameobject.GetComponent<IwashiManager>();

        // 回転用変数の初期化
        TarRot = Quaternion.identity;

        rotate = false;

        Parent = iwashimanager.GetParent();

        if (Parent)
        {
            iwashimanager.SetIwashi(transform.localPosition);
        }

        transform.localRotation = iwashimanager.GetTargetRotation();

        count = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 前方向に移動
        transform.Translate(0.0f, 0.0f, Speed * Time.deltaTime, Space.Self);

        // 方向転換条件
        //if (transform.localPosition.z < 4.0f && !change)
        //if (Input.GetKeyDown("space")/* && !change && !rotate*/)
        if (TimeLimit < count)
        {
            rotate = true;

            count = 0.0f;
        }

        // 回転角度設定
        if (rotate)
        {
            if (Parent)
            {
                // iwashimanagerのSetIwashiメソッドを呼び出す(目的角度を取得)
                iwashimanager.SetIwashi(/*iwashimanager.GetTargetPosition(), */transform.localPosition);
            }

            // 
            TarRot = iwashimanager.GetTargetRotation();

            rotate = false;
            change = true;
        }

        // 方向転換
        if (change)
        {
            // 回転(滑らかに)
            transform.localRotation = Quaternion.Slerp(transform.localRotation, TarRot, Time.deltaTime * TurningSpeed);

            if (CheckRotate(TarRot, transform.localRotation))
            {
                change = false;
            }
        }

        count += Time.deltaTime;
    }


    // いわしの角度が目的角度に達したかどうか確認する関数
    bool CheckRotate(Quaternion targetRotation, Quaternion Rotation)
    {
        if (targetRotation.x == Rotation.x)
        {
            if (targetRotation.y == Rotation.y)
            {
                if (targetRotation.z == Rotation.z)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
