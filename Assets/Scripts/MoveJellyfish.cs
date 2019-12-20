using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimaroidLib;

public class MoveJellyfish : MonoBehaviour
{
    public float LeftSpeed = 1.0f;
    public float FallSpeed =0.3f;
    public float InitUpSpeed = 1.0f;
    public float BlendTime = 0.3f;
    public float UpDamp = 1.0f;

    private float UpSpeed;
    private bool IsMoveUp = false;
    private Timer timer = new Timer();

    private void Start()
    {
        timer.Reset(BlendTime);
        UpSpeed = InitUpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        timer++;

        Vector3 up = (UpSpeed-=(UpDamp * Time.deltaTime)) * Vector3.up;
        Vector3 down = FallSpeed * Vector3.down;
        Vector3 speed = LeftSpeed*Vector3.left + Vector3.Lerp(up, down, IsMoveUp ? timer.InverseProgress : timer.Progress);
        transform.Translate(speed * Time.deltaTime, Space.Self);
    }

    public void OnMoveUp()
    {
        IsMoveUp = true;
        UpSpeed = InitUpSpeed;
        timer.Reset();
    }

    public void OnStopMoveUp()
    {
        IsMoveUp = false;
        timer.Reset();
    }
}
