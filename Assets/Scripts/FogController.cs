using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    private float startTime;
    private float nowTime;
    private const float changeTime = 12.0f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        RenderSettings.fog = true;
    }

    // Update is called once per frame
    void Update()
    {
        nowTime = Time.realtimeSinceStartup - startTime;
        if ((int)nowTime == (int)changeTime)
        {
            RenderSettings.fog = false;
        }
    }
}
