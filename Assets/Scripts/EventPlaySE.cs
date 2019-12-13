using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlaySE : MonoBehaviour
{
    private AudioSource se;
    public float playTime;
    private float startTime;
    private float nowTime;


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        se = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        nowTime = Time.realtimeSinceStartup - startTime;

        if((int)nowTime == (int)playTime)
        {
            se.Play();
        }
    }
}
