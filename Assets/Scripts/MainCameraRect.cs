using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraRect : MonoBehaviour
{
    [SerializeField]
    private float ChangeTime = 5.0f;

    [SerializeField]
    private Camera cam;

    private float Count;
    private bool EndEvent;


    // Start is called before the first frame update
    void Start()
    {
        Count = 0.0f;
        EndEvent = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeTime > Count)
        {
            Count += Time.deltaTime;
        }
        else
        {
            cam.rect = new Rect(0, 0, 0, 0);

            EndEvent = true;
        }
    }

    public bool GetEnd()
    {
        return EndEvent;
    }
}
