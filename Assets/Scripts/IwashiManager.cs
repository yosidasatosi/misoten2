﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IwashiManager : MonoBehaviour
{
    public GameObject iwashiPrefab;

    public int pop = 50;

    public float PopTime = 2.0f;

    [System.Serializable]
    public class MoveData
    {
        public Vector3 TargetPosition;
        public float SwimSpeed;
        public float TurnSpeed;
        public float TimeLimit;
    }

    public List<Vector3> TargetPosition = new List<Vector3>();

    public List<MoveData> MoveDatas = new List<MoveData>();

    public List<GameObject> IwashiModel = new List<GameObject>();

    private int TarNo;
    private int ParentNo;

    private float PopCount;

    private bool Parent;
    private bool Pop;

    private bool Delete;

    private Quaternion TargetRotation;

    private GameObject[] Iwashi;

    // Start is called before the first frame update
    void Start()
    {
        TarNo = 0;
        ParentNo = 0;

        Parent = false;
        Pop = false;
        Delete = false;

        TargetRotation = Quaternion.identity;

        Iwashi = new GameObject[pop];
    }

    // Update is called once per frame
    void Update()
    {
        if (!Delete)
        {
            if (!Pop)
            {
                //if (Input.GetButtonDown("Fire1"))
                if (PopTime < PopCount)
                {
                    Quaternion Rot = Quaternion.identity;

                    //for (int i = 0; i < IwashiModel.Count; i++)
                    for (int i = 0; i < pop; i++)
                    {
                        // プレファブからIwashiオブジェクトを生成
                        Iwashi[i] = (GameObject)Instantiate(
                            iwashiPrefab,               // 生成するプレファブ設定
                            Camera.main.transform       // 親設定
                            );

                        // 初期位置設定
                        Iwashi[i].transform.localPosition = IwashiModel[i].transform.localPosition;
                        Iwashi[i].transform.localRotation = IwashiModel[i].transform.localRotation;
                    }

                    Pop = true;
                }
                else
                {
                    UpdateTimer();
                }
            }
        }
    }

    private void UpdateTimer()
    {
        PopCount += Time.deltaTime;
    }

    // 目的角度設定
    public void SetIwashi(/*Vector3 targetposition, */Vector3 position)
    {
        //TarNo %= TargetPosition.Count;

        if (!CheckTarget())
        {
            // 目的角度設定
            TargetRotation = Quaternion.LookRotation(TargetPosition[TarNo++] - position);
        }

        //Debug.Log("TarNo" + TarNo);
    }

    // 
    public bool CheckTarget()
    {
        if (TarNo >= TargetPosition.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 
    public MoveData SetMoveData()

    {
        return MoveDatas[0];
    }

    // 
    public Quaternion GetTargetRotation()
    {
        return TargetRotation;
    }

    // 
    public bool GetParent()
    {
        ParentNo %= pop;

        if (ParentNo == 0)
        {
            Parent = true;
        }
        else
        {
            Parent = false;
        }

        ParentNo++;

        return Parent;
    }

    // 
    public void DeleteClone()
    {
        TarNo = 0;
        //seisei = false;

        for (int i = 0; i < pop; i++)
        {
            GameObject.Destroy(Iwashi[i]);
        }

        Delete = true;
    }

    // 
    public bool GetDelete()
    {
        return Delete;
    }
}
