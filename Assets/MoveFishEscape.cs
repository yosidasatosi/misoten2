﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFishEscape : MonoBehaviour
{

    public float Speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(0.0f, 0.0f, Speed * Time.deltaTime, Space.Self);


        //if (transform.localPosition.x <= 23.5f)
        //{

        //}
    }
}
