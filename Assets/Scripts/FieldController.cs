using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public List<GameObject> FieldList;
    public Camera MainCamera;

    private float FieldDistance;

    void Start()
    {
        if(FieldList.Count != 2)
        {
            return;
        }

        // フィールド間の初期距離を記録
        FieldDistance = FieldList[1].transform.position.z - FieldList[0].transform.position.z;

        // メインカメラが当たるときのイベント処理をバインド
        var FieldSwapColliderA = FieldList[0].GetComponentInChildren<FieldSwapCollider>();
        if(FieldSwapColliderA)
        {
            FieldSwapColliderA.OnMainCameraHit = () => TranslateField(1);
        }

        var FieldSwapColliderB = FieldList[1].GetComponentInChildren<FieldSwapCollider>();
        if (FieldSwapColliderB)
        {
            FieldSwapColliderB.OnMainCameraHit = () => TranslateField(0);
        }
    }

    private void TranslateField(int index)
    {
        if(index < FieldList.Count)
        {
            FieldList[index].transform.Translate(0.0f, 0.0f, 2.0f * FieldDistance);
        }
    }

}
