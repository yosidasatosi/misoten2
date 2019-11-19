using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public new Camera camera;
    public Vector3 moveRange = new Vector3(5.0f, 3.0f, 8.0f);
    public float Yoffset = 0.0f;
    public float InitAnimSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        var animator = GetComponentInChildren<Animator>();
        animator?.SetFloat("Speed", InitAnimSpeed);
    }

    // Update is called once per frame

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        transform.Translate(inputX, inputY, 0.0f);
    }

    void LateUpdate()
    {
        Vector3 cameraPos = camera.transform.position;
        transform.position = new Vector3
        {
            x = Mathf.Clamp(transform.position.x, cameraPos.x-moveRange.x, cameraPos.x+moveRange.x),
            y = Mathf.Clamp(transform.position.y, cameraPos.y-moveRange.y+Yoffset, cameraPos.y+moveRange.y+Yoffset),
            z = cameraPos.z+moveRange.z
        };       
    }
}
