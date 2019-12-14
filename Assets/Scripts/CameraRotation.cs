using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    private GameObject titleLogo;
    private float rotAngle = 10f;
    public bool rotStart;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        titleLogo = GameObject.Find("TitleLogo");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rotStart)
        {
            transform.RotateAround(titleLogo.transform.position, Vector3.up, rotAngle * Time.deltaTime);
        }

    }

    public bool GetRotStart()
    {
        return rotStart;
    }

    public void CameraRot()
    {
        //animator.SetBool("AnimationStop", true);
        rotStart = true;
    }
}
