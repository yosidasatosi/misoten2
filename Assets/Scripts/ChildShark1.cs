using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildShark1 : MonoBehaviour
{
    public float Speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, 0.0f, Speed * Time.deltaTime, Space.Self);
    }
}
