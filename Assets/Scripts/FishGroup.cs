using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGroup : MonoBehaviour
{
    //群の数
    public int MaxChild = 30;
    //群の個体
    public GameObject BoidsChild;
    public GameObject[] BoidsChildren;

    //群のボス
    public GameObject BoidsBoss;

    //群の中心
    public GameObject BoidsCenter;

    //乱れ係数
    public float Turbulence = 1f;

    public int Distance = 5;


    // Start is called before the first frame update
    void Start()
    {
        
        this.BoidsChildren = new GameObject[MaxChild];

        for(int i = 0; i < MaxChild; i++)
        {

            //配列に量産するGameObjectをいれる
            this.BoidsChildren[i] = GameObject.Instantiate(BoidsChild) as GameObject;

            //量産するGameObjectの位置　（ランダム）
            this.BoidsChildren[i].transform.position
                = new Vector3(Random.Range(-5f, 5f),
                              this.BoidsChild.transform.position.y,
                              Random.Range(-5f, 5f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = Vector3.zero;

        foreach(GameObject child in this.BoidsChildren)
        {
            
            center += child.transform.position;
        }

       
        center /= (BoidsChildren.Length - 1);               //群の数−１で割る
        center += this.BoidsBoss.transform.position;        //centerに群のボスの座標を加算
        center /= 2;
        this.BoidsCenter.transform.position = center;       //BoidsCenterにcenterに代入


        foreach (GameObject child in this.BoidsChildren)
        {

            Vector3 dirToCenter = (center - child.transform.position).normalized;       //centerの座標とchildの座標の差分
            Vector3 direction = (child.GetComponent<Rigidbody>().velocity.normalized * this.Turbulence
                               + dirToCenter * (1 - this.Turbulence)).normalized;
            direction *= Random.Range(2f, 5f);
            child.GetComponent<Rigidbody>().velocity = direction;
        }

        foreach(GameObject childA in this.BoidsChildren)
        {
            foreach(GameObject childB in this.BoidsChildren)
            {
                //比較対象が同じならcontinue
                if (childA == childB)
                {
                    continue;
                }

                Vector3 diff= childA.transform.position - childB.transform.position;

                if (diff.magnitude < Random.Range(2, this.Distance))
                {
                    childA.GetComponent<Rigidbody>().velocity =
                        diff.normalized * childA.GetComponent<Rigidbody>().velocity.magnitude;
                }


            }
        }

        Vector3 averageVelocity = Vector3.zero;

        foreach(GameObject child in this.BoidsChildren)
        {
            averageVelocity += child.GetComponent<Rigidbody>().velocity;
        }

        averageVelocity /= this.BoidsChildren.Length;

        foreach(GameObject child in this.BoidsChildren)
        {
            child.GetComponent<Rigidbody>().velocity = child.GetComponent<Rigidbody>().velocity * this.Turbulence
                + averageVelocity * (1f - this.Turbulence);
        }

    }
}