using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkouMove : MonoBehaviour
{
    public Vector3 position;
    
    public float Zspeed = 10.0f;
    public float Yspeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, Yspeed * Time.deltaTime, Zspeed * Time.deltaTime, Space.Self);
    }
}
