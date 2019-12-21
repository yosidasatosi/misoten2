using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnim : MonoBehaviour
{
    public Image image;
    private Sprite resource1;
    private Sprite resource2;
    private bool chFlg = false;
    private float time;
    private float ChangeTime = 0.05f;
    private Texture2D texture1;
    private Texture2D texture2;

    void Start()
    {
        texture1 = Resources.Load("button_push1") as Texture2D;
        texture2 = Resources.Load("button_push2") as Texture2D;

        resource1 = Sprite.Create(texture1,
                               new Rect(0, 0, texture1.width, texture1.height),
                               Vector2.zero);
        resource2 = Sprite.Create(texture2,
                       new Rect(0, 0, texture2.width, texture2.height),
                       Vector2.zero);

    }

    void Update()
    {

        if(Time.timeScale != 1.0f)
        {
            ChangeTime = 0.05f;
        }
        else
        {
            ChangeTime = 0.1f;
        }

        time += Time.deltaTime;

        if (time > ChangeTime)
        {
            if (!chFlg)
            {
                image.sprite = resource1;
                chFlg = true;
            }
            else
            {
                image.sprite = resource2;
                chFlg = false;
            }

            time = 0;
        }
    }
}