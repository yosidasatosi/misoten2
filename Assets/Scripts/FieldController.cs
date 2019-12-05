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
        if(FieldList.Count < 1 )
        {
            return;
        }
        
        // フィールド間の初期距離を記録
        FieldDistance = FieldList[FieldList.Count - 1].transform.position.z - FieldList[0].transform.position.z;
        
        for (int i = 0; i < FieldList.Count; i++)
        {
            // メインカメラが当たるときのイベント処理をバインド
            var FieldSwapCollider = FieldList[i].GetComponentInChildren<FieldSwapCollider>();
            if (FieldSwapCollider)
            {
                if (i == (FieldList.Count - 1))
                {
                    if (FieldList.Count == 2)
                    {
                        FieldSwapCollider.OnMainCameraHit = () => TranslateField(0);
                    }
                    else
                    {
                        FieldSwapCollider.OnMainCameraHit = () => TranslateField(1);
                    }
                }
                else if (i == 1)
                {
                    FieldSwapCollider.OnMainCameraHit = () => TranslateField(0);
                }
                else if (i == 0)
                {
                    FieldSwapCollider.OnMainCameraHit = () => TranslateField(FieldList.Count - 1);
                }
            }
        }

        //var FieldSwapColliderB = FieldList[1].GetComponentInChildren<FieldSwapCollider>();
        //if (FieldSwapColliderB)
        //{
        //    FieldSwapColliderB.OnMainCameraHit = () => TranslateField(0);
        //}

        //var FieldSwapColliderC = FieldList[2].GetComponentInChildren<FieldSwapCollider>();
        //if (FieldSwapColliderC)
        //{
        //    FieldSwapColliderC.OnMainCameraHit = () => TranslateField(1);
        //}
    }

    private void TranslateField(int index)
    {
        if(index < FieldList.Count)
        {
            FieldList[index].transform.Translate(0.0f, 0.0f, 2.0f * FieldDistance);
        }
    }

}
