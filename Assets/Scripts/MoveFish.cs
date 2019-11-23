using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFish : MonoBehaviour
{
    //prefabパブリック変数
    public GameObject fishPrefab;

    //魚の数
    public int numFish = 20;

    //魚格納配列
    public GameObject[] allFish;

    //魚の泳ぐ範囲
    public Vector3 swimLimits = new Vector3(5,5,5);

    //魚の速度
    [Header("Fish Settings")]
    [Range(0.0f,5.0f)]
    public float minSpeed;
    [Range(0.0f,5.0f)]
    public float maxSpeed;

    //周囲の魚との距離
    [Range(1.0f,10.0f)]
    public float neighbourDistance;

    [Range(0.0f,5.0f)]
    public float rotationSpeed;

    public Flock flock;

    // Start is called before the first frame update
    void Start()
    {
        //魚の格納配列を魚の数分用意
        allFish = new GameObject[numFish];

        //魚の数分回す
        for(int i = 0; i < numFish; i++)
        {
            //群行動マネージャーの位置
            Vector3 pos = this.transform.position+new Vector3(Random.Range(-swimLimits.x,swimLimits.x),
                                                              Random.Range(-swimLimits.y, swimLimits.y),
                                                              Random.Range(-swimLimits.z, swimLimits.z));

            //計算された位置で配列に格納
            allFish[i] = (GameObject) Instantiate(fishPrefab,pos,Quaternion.identity);
            allFish[i].GetComponent<Flock>().isMove = this;
        }


    }

    // Update is called once per frame
    void Update()
    {
    }


}
