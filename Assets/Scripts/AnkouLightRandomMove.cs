using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkouLightRandomMove : MonoBehaviour
{
    public Vector3 Range;
    public Vector3 Speed;
    public Vector3 RandomParam;

    private float timer;
    private Vector3 StartPosition;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        StartPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.localPosition = StartPosition + 
            new Vector3(
                Range.x * Mathf.Sin(timer * Speed.x + RandomParam.x),
                Range.y * Mathf.Sin(timer * Speed.y + RandomParam.y),
                Range.z * Mathf.Sin(timer * Speed.z + RandomParam.z));
    }
}
