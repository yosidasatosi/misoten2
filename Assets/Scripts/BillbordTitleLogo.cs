﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillbordTitleLogo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = Camera.main.transform.position;
        p.y = transform.position.y;
        this.transform.LookAt(p);

    }
}
