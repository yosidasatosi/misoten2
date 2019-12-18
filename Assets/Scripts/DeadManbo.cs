using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadManbo : MonoBehaviour
{
    
    public float Speed = 3.0f;

    private float timer = 0.0f ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 4.0f)
        {
            transform.Translate(0.0f, Speed * Time.deltaTime, 0.0f, Space.Self);
        }
    }
}
