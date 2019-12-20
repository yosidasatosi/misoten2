using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnim : MonoBehaviour
{
    private bool chFlg = false;
    private float time;
    private Texture2D texture;
    void Update()
    {
        time += Time.deltaTime;

        if (time > 1.0f)
        {
            if (!chFlg)
            {
                texture = Resources.Load("button_push1") as Texture2D;
                GetComponent<Image>().sprite = Sprite.Create(texture,
                                       new Rect(0, 0, texture.width, texture.height),
                                       Vector2.zero);
                chFlg = true;
            }
            else
            {
                texture = Resources.Load("button_push2") as Texture2D;
                GetComponent<Image>().sprite = Sprite.Create(texture,
                                       new Rect(0, 0, texture.width, texture.height),
                                       Vector2.zero);
                chFlg = false;
            }

            time = 0;
        }
    }
}