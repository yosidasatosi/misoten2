using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IwashiManager : MonoBehaviour
{
    public GameObject iwashiPrefab;

    public int pop;

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
    private bool Parent;

    private Quaternion TargetRotation;

    // Start is called before the first frame update
    void Start()
    {
        TarNo = 0;
        ParentNo = 0;
        Parent = false;
        TargetRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Quaternion Rot = Quaternion.identity;

            //for (int i = 0; i < IwashiModel.Count; i++)
            for (int i = 0; i < pop; i++)
            {
                //Debug.Log("ManagerUpdate");

                // プレファブからIwashiオブジェクトを生成
                GameObject Iwashi = (GameObject)Instantiate(
                    iwashiPrefab,               // 生成するプレファブ設定
                    Camera.main.transform       // 親設定
                    );

                // 初期位置設定
                Iwashi.transform.localPosition = IwashiModel[i].transform.localPosition;
                Iwashi.transform.localRotation = IwashiModel[i].transform.localRotation;
            }
        }
    }

    // 目的角度設定
    public void SetIwashi(/*Vector3 targetposition, */Vector3 position)
    {
        TarNo %= TargetPosition.Count;

        // 目的角度設定
        TargetRotation = Quaternion.LookRotation(TargetPosition[TarNo++] - position);
    }

    public MoveData SetMoveData()

    {
        return MoveDatas[0];
    }

    public Quaternion GetTargetRotation()
    {
        return TargetRotation;
    }

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
}
