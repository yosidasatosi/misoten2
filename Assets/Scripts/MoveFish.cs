using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFish : MonoBehaviour
{
    //prefabパブリック変数
    [SerializeField]
    public GameObject fishPrefab;

    //魚の数
    [SerializeField]
    public int numFish = 20;

    //魚格納配列
    [SerializeField]
    public GameObject[] allFish;

    //魚の泳ぐ範囲
    [SerializeField]
    public Vector3 swimLimits = new Vector3(5,5,5);

    //魚の速度
    [Header("Fish Settings")]
    [Range(0.0f,5.0f)]
    [SerializeField]
    public float minSpeed;
    [Range(0.0f,5.0f)]
    [SerializeField]
    public float maxSpeed;


    [Range(1.0f,10.0f)]
    [SerializeField]
    public float neighbourDistance;

    [Range(0.0f,5.0f)]
    [SerializeField]
    public float rotationSpeed;



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
