using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnData
    {
        public GameObject EnemyPrefab;
        public float Time;
        public Vector3 Position;
        public Vector3 Rotation;
    }

    public List<SpawnData> SpawnDataList = new List<SpawnData>();

    private List<float> Timers;

    private void Start()
    {
        Timers = new List<float>(SpawnDataList.Count);
        foreach(var data in SpawnDataList)
        {
            Timers.Add(data.Time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<SpawnDataList.Count;i++)
        {
            var data = SpawnDataList[i];
            var timer = Timers[i];
            if (timer <= 0.0f)
            {
                continue;
            }

            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                SpawnEnemy(data);
            }

            SpawnDataList[i] = data;
            Timers[i] = timer;
        }

    }

    private void SpawnEnemy(SpawnData data)
    {
        var enemy = Instantiate(data.EnemyPrefab, Camera.main.transform);
        enemy.transform.localPosition = data.Position;
        enemy.transform.rotation = Quaternion.Euler(data.Rotation);
    }
}
