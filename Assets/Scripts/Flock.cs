using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    //MoveFishスクリプトへのリンク
    public MoveFish isMove;

    //速度
    private float speed;



    // Start is called before the first frame update
    void Start()
    {
        //speedにランダムで速度を宣言
        speed = Random.Range(isMove.minSpeed, isMove.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //魚を前方に移動 Z軸
        transform.Translate(-Time.deltaTime * speed, 0, 0);
        MoveRules();

    }

    //魚の群行動の仕様
    void MoveRules()
    {
        //現在の魚を保持する配列
        GameObject[] gos;
        gos = isMove.allFish;
        //行動エリアの平均中心
        Vector3 vcenter = Vector3.zero;
        //平均回避ベクトル
        Vector3 vavoid = Vector3.zero;

        //グループのグローバル速度　平均的なグループが移動する速度　計算に使用
        float gSpeed = 0.01f;

        //それぞれの魚からどれだけ離れているか確認後グループにどれだけ近いか特定
        float nDistance;

        //グループ内の魚の数
        int groupSize = 0;

        //1匹ずつループ
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                //距離
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                //平均計算
                if (nDistance <= isMove.neighbourDistance)
                {
                    //vcenteに格納される中心に加算
                    vcenter += go.transform.position;
                    //グループサイズインクリメント
                    groupSize++;

                    //距離が１.０より小さかったら
                    if (nDistance < 0.5f)
                    {
                        //他の魚から離れる処理
                        vavoid = vavoid + (this.transform.position - go.transform.position);

                    }

                    //群の中の魚の速度の合計　魚の速度をgspeedに加算
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {

            //平均の中心をグループサイズで除算
            vcenter = vcenter / groupSize;

            //魚の速度
            speed = gSpeed / groupSize;

            //魚が進む方向
            Vector3 direction = (vcenter + vavoid) - transform.position;

            //魚を必要な方向に回転
            if (direction != Vector3.zero)
            {
                Debug.Log("rotation");
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                   Quaternion.LookRotation(direction),
                                   isMove.rotationSpeed * Time.deltaTime);
            }
        }


    }
}
