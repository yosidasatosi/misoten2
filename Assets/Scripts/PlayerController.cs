using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1,4)]
    public int PlayerNo = 1;
    public Vector3 moveRange = new Vector3(5.0f, 3.0f, 8.0f);
    public float Speed = 10.0f;
    public float Yoffset = 0.0f;
    public float InitAnimSpeed = 1.0f;

    private Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        var animator = GetComponentInChildren<Animator>();
        animator?.SetFloat("Speed", InitAnimSpeed);

        MainCamera = Camera.main;
    }

    // Update is called once per frame

    private void Update()
    {
        Vector3 input;
        input.x = Input.GetAxis("LeftStickX" + PlayerNo);
        input.y = Input.GetAxis("LeftStickY" + PlayerNo);
        input.z = 0.0f;

        //float inputX = Input.GetAxis("LeftStickX"+PlayerNo);
        //float inputY = Input.GetAxis("LeftStickY"+PlayerNo);
        transform.Translate(input * Speed * Time.deltaTime);
    }

    void LateUpdate()
    {
        Vector3 cameraPos = MainCamera.transform.position;
        transform.position = new Vector3
        {
            x = Mathf.Clamp(transform.position.x, cameraPos.x-moveRange.x, cameraPos.x+moveRange.x),
            y = Mathf.Clamp(transform.position.y, cameraPos.y-moveRange.y+Yoffset, cameraPos.y+moveRange.y+Yoffset),
            z = cameraPos.z+moveRange.z
        };       
    }
}
