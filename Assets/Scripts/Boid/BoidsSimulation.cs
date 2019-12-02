using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class BoidsSimulation : MonoBehaviour
{
    [SerializeField]
    int boidCount = 100;

    [SerializeField]
    GameObject boidPrefab;

    [SerializeField]
    Param param;

    

    

    //魚の泳ぐ範囲Vector3 swimLimits = new Vector3(5, 5, 5);

    private List<Boid> boids = new List<Boid>();


    //Listを読み取り専用に
    public ReadOnlyCollection<Boid> boidsA
    {
        get { return boids.AsReadOnly(); }
    }

    private void Start()
    {
    }

    //作成
    void AddBoid()
    {
        Vector3 pos = this.transform.position + Random.insideUnitSphere;
        var go = Instantiate(boidPrefab, pos, Random.rotation);     //fishPrefabを作成
        go.transform.SetParent(transform);                                              //goのtransformに親のtransformを設定
        var boid = go.GetComponent<Boid>();
        boid.simulation = this;
        boid.param = param;
        boids.Add(boid);

    }

    //削除
    void RemoveBoid()
    {
        if (boids.Count == 0)
        {
            return;
        }

        //配列の最後
        var lastIndex = boids.Count - 1;
        var boid = boids[lastIndex];
        Destroy(boid.gameObject);
        boids.RemoveAt(lastIndex);

    }


    // Update is called once per frame
    void Update()
    {
        while (boids.Count < boidCount)
        {
            AddBoid();
        }
        while (boids.Count > boidCount)
        {
            RemoveBoid();
        }

    }

    private void OnDrawGizmos()
    {
        if (!param)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * param.wallScall);
    }
}
