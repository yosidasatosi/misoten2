using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkouMove : MonoBehaviour
{
    public Vector3 position;
    
    public float Zspeed = 10.0f;
    public float Yspeed = 2.0f;

    public float SlowMotionScale = 0.1f;
    public float BeginSlowMotionTime = 0.5f;
    public float SlowMotionDuration = 0.5f;

    private enum EState
    {
        Init,
        SlowMo,
        Final
    }
    private EState State;
    private float Timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, Yspeed * Time.deltaTime, Zspeed * Time.deltaTime, Space.Self);
        Timer += Time.deltaTime;
        
        switch(State)
        {
            case EState.Init:
                if(Timer >= BeginSlowMotionTime)
                {
                    UIManager.Instance.AvoidanceUI.SetActive(true);
                    LedState.Instance.Set(LedState.Situation.DANGER);
                    State++;
                    Timer = 0.0f;
                    Time.timeScale = SlowMotionScale;
                }
                break;

            case EState.SlowMo:
                if(Timer >= SlowMotionDuration*Time.timeScale)
                {
                    UIManager.Instance.AvoidanceUI.SetActive(false);
                    LedState.Instance.Set(MainSceneController.Instance.DefaultLedSituation);
                    State++;
                    Timer = 0.0f;
                    Time.timeScale = 1.0f;
                    GetComponent<Collider>().enabled = true;
                }
                break;
        }
    }
}
