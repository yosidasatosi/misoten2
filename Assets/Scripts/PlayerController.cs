using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1,4)]
    public int PlayerNo = 1;
    public bool HorizontalScrollMode = false;
    public bool EnableLimitMoveRange = true;
    public Vector3 moveRange = new Vector3(5.0f, 3.0f, 8.0f);
    public float Speed = 10.0f;
    public float Yoffset = 0.0f;
    public float InitAnimSpeed = 1.0f;

    public bool EnableInput { get; set; }
    public bool IsAvoiding { get; private set; }

    private Camera MainCamera;
    private Vector3 LastPosition;
    private Vector3 AvoidencePosition;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        var animator = GetComponentInChildren<Animator>();
        animator?.SetFloat("Speed", InitAnimSpeed);

        MainCamera = Camera.main;
        EnableInput = true;
        IsAvoiding = false;
    }

    // Update is called once per frame

    private void Update()
    {
        if(EnableInput)
        {
            // 移動処理
            Vector3 input;
            input.x = Input.GetAxis("LeftStickX" + PlayerNo);
            input.y = Input.GetAxis("LeftStickY" + PlayerNo);
            input.z = 0.0f;

            if (HorizontalScrollMode)
            {
                input.z = input.x;
                input.x = 0.0f;
            }

            transform.Translate(input * Speed * Time.deltaTime, Space.Self);

            // アクション処理
            if(Input.GetAxis("Action" + PlayerNo) > 0.0f)
            {
                StartAvoidance();
            }
        }
    }

    void LateUpdate()
    {
        Vector3 cameraPos = MainCamera.transform.position;
        if(HorizontalScrollMode)
        {
            if (EnableLimitMoveRange)
            {
                transform.position = new Vector3
                {
                    x = cameraPos.x + moveRange.x,
                    y = Mathf.Clamp(transform.position.y, cameraPos.y - moveRange.y + Yoffset, cameraPos.y + moveRange.y + Yoffset),
                    z = Mathf.Clamp(transform.position.z, cameraPos.z - moveRange.z, cameraPos.z + moveRange.z)
                };
            }
            else
            {
                transform.position = new Vector3
                {
                    x = cameraPos.x + moveRange.x,
                    y = transform.position.y,
                    z = transform.position.z
                };
            }
        }
        else
        {
            if (EnableLimitMoveRange)
            {
                transform.position = new Vector3
                {
                    x = Mathf.Clamp(transform.position.x, cameraPos.x - moveRange.x, cameraPos.x + moveRange.x),
                    y = Mathf.Clamp(transform.position.y, cameraPos.y - moveRange.y + Yoffset, cameraPos.y + moveRange.y + Yoffset),
                    z = cameraPos.z + moveRange.z
                };
            }
            else
            {
                transform.position = new Vector3
                {
                    x = transform.position.x,
                    y = transform.position.y,
                    z = cameraPos.z + moveRange.z
                };
            }
        }
    }

    void StartAvoidance()
    {
        if(IsAvoiding || !UIManager.Instance.AvoidanceUI.activeSelf)
        {
            return;
        }

        EnableInput = false;
        EnableLimitMoveRange = false;
        IsAvoiding = true;
        LastPosition = transform.localPosition;
        AvoidencePosition = new Vector3(transform.localPosition.x > 0 ? 12.0f : -12.0f, 0.0f, 0.0f);
        GetComponent<Collider>().enabled = false;

        StartCoroutine("AvoidanceMove", false);
    }

    IEnumerator AvoidanceMove(bool IsReturn)
    {
        Timer = 0.0f;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            Timer += Time.deltaTime;
            progress = Timer / 0.3f;
            transform.localPosition = Vector3.Lerp(LastPosition, AvoidencePosition, IsReturn ? 1.0f - progress : progress );
            yield return null;
          
        }

        if(IsReturn)
        {
            EnableInput = true;
            EnableLimitMoveRange = true;
            IsAvoiding = false;
            GetComponent<Collider>().enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine("AvoidanceMove", true);
        }
    }
}
