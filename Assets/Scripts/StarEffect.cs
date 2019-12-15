using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEffect : MonoBehaviour
{
    //　出現させるエフェクト
    [SerializeField]
    public GameObject effectObject;
    public Camera mainCamera;
    //　エフェクトを消す秒数
    [SerializeField]
    public float deleteTime;
    //　エフェクトの出現位置のオフセット値
    [SerializeField]
    private float offset;
    public bool use = false;

    // Update is called once per frame
    void Update()
    {
        if (use)
        {
            //　ゲームオブジェクト登場時にエフェクトをインスタンス化
            var instantiateEffect = GameObject.Instantiate(effectObject, transform.position, Quaternion.identity) as GameObject;
            instantiateEffect.transform.parent = mainCamera.transform;
            Destroy(instantiateEffect, deleteTime);
            use = false;
        }
    }
}
