﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKuzira : MonoBehaviour
{
    public float Speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Speed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
    }
}
