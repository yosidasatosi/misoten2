using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public BoidsSimulation simulation { get; set; }
    public Param param { get; set; }
    public Vector3 pos { get; private set; }
    public Vector3 velocity { get; private set; }

    GameObject groupCenter;
    Vector3 wallScall;

    //速度
    private Vector3 accel = Vector3.zero;
    
    List<Boid> neighbors = new List<Boid>();

    //角度
    private Quaternion angle;


    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクトをy軸に90度回転
        transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Euler(0, 90, 0);
        
        pos = transform.position;
        velocity = transform.forward * param.initSpeed;
        wallScall =new Vector3(transform.position.x+param.wallScall,
                               transform.position.y+param.wallScall,
                               transform.position.z+param.wallScall);
    }

    // Update is called once per frame
    void Update()
    {
        //近隣個体を探してneighborsリスト更新
        UpdateNeighbors();

        //壁に当たりそうになったら向きを変える
        UpdateWalls();

        //近隣の個体から離れる
        UpdateSeparation();

        //近隣の個体と速度を合わせる
        UpdateAlignment();

        //近隣の個体の中心に移動する
        UpdateCohesion();

        //上記４つの結果更新されたaccelをVelocityに反映
        UpdateMove();
    }

    //移動
    void UpdateMove()
    {
        velocity += accel * Time.deltaTime;
        //向きベクトル
        var dir = velocity.normalized;
        //移動速度
        var speed = velocity.magnitude;
        velocity = Mathf.Clamp(speed, param.minSpeed, param.maxSpeed) * dir;
        pos += velocity * Time.deltaTime;

        //回転
        var rot = Quaternion.LookRotation(velocity);
        //位置と速度の設定
        transform.SetPositionAndRotation(pos,rot);

        accel = Vector3.zero;


    }

    //壁に当たりそうになったら向きを変える
    void UpdateWalls()
    {

        if (!simulation)
        {
            return;
        }

        //範囲スケール
        var scale = wallScall * 0.5f;


        accel += CalcAccelAgainstWall(-scale.x - pos.x, Vector3.right) +
                 CalcAccelAgainstWall(-scale.y - pos.y, Vector3.up) +
                 CalcAccelAgainstWall(-scale.z - pos.z, Vector3.forward) +
                 CalcAccelAgainstWall(scale.x - pos.x, Vector3.left) +
                 CalcAccelAgainstWall(scale.y - pos.y, Vector3.down) +
                 CalcAccelAgainstWall(scale.z - pos.z, Vector3.back);




    }


    Vector3 CalcAccelAgainstWall(float distance,Vector3 dir)
    {
        //壁との距離がwallScallより小さい
        if (distance < param.wallScall)
        {
            return dir * (param.wallWeight / Mathf.Abs(distance / param.wallScall));
        }
        else
        {
            return Vector3.zero;
        }



    }

    //近隣個体を探してneighborsリスト更新
    void UpdateNeighbors()
    {
        neighbors.Clear();

        if (!simulation)
        {
            return;
        }

        //検索範囲角度　Search range angle
        var prodThresh = Mathf.Cos(param.neighborFov * Mathf.Deg2Rad);
        //検索範囲距離 Search range distance
        var distThresh = param.neighborDistance;

        //listの総当たり
        foreach(var other in simulation.boidsA)
        {
            if (other == this)
            {
                continue;
            }

            //近隣の個体との距離 Distans neighboring individuals.
            var to = other.pos - pos;
            var dist = to.magnitude;

            //近隣個体との距離が設定距離より内
            if (dist < distThresh)
            {
                var dir = to.normalized;                //正規化ベクトル velocity vector.
                var fwd = velocity.normalized;          //速度ベクトルの正規化　Normalize Velocity vector.
                var prod = Vector3.Dot(fwd, dir);       //二つのベクトルの内積 Inner product of two vectors. 


                //範囲内の個体をlistに追加　Add individuals within range to neighbor's list.
                if (prod > prodThresh)
                {
                    neighbors.Add(other);
                }
            }
        }
    }

    //近隣の個体から離れる Stay away from nearby individuals.
    void UpdateSeparation()
    {
        //近隣に個体が居ない　There are no individuals in the vicinity
        if (neighbors.Count == 0)
        {
            return;
        }

        Vector3 force = Vector3.zero;                    //近隣の個体から離れる力　The power　to move away from nearby individuals.

        //近隣の個体を比較
        foreach (var neignbor in neighbors)
        {
            force += (pos - neignbor.pos).normalized;    //自分の座標と近隣個体の座標の距離の正規化ベクトルを加算
        }


        force /= neighbors.Count;                       //離れる力を近隣の個体でわる

        accel += force * param.separationWeight;
    }

    //近隣の個体と速度を合わせる
    void UpdateAlignment()
    {
        if (neighbors.Count == 0)
        {
            return;
        }

        var averageVelocity = Vector3.zero;

        //List総当たり
        foreach (var neighbor in neighbors)
        {
            averageVelocity += neighbor.velocity;
        }
        averageVelocity /= neighbors.Count;

        accel += (averageVelocity - velocity) * param.alignmentWeight;
    }

    //近隣の個体の中心に移動する
    void UpdateCohesion()
    {
        if (neighbors.Count == 0)
        {
            return;
        }

        var averagePos = Vector3.zero;

        //List総当たり
        foreach (var neighbor in neighbors)
        {
            averagePos += neighbor.pos;
        }

        averagePos /= neighbors.Count;

        accel += (averagePos - pos) * param.cohesionWeight;
    }
}
