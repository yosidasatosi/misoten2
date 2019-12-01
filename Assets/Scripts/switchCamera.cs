using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Camera lastCamera;
    public float changeTime = 0.0f;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        lastCamera.enabled = false;
        startTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        float nowTime = Time.realtimeSinceStartup - startTime;

        if ((int)nowTime == changeTime)
        {
            mainCamera.enabled = false;
            lastCamera.enabled = true;
        }
    }
}
