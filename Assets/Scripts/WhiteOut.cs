using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteOut : MonoBehaviour
{
    public Image image;
    private float alpha;
    private float startTime;
    private float nowTime;
    public float startFade;
    public float endFade;
    public float fadeTime;
    private Color changedColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    
    // Start is called before the first frame update
    void Start()
    {
        if (!image.IsActive())
        {
            image.enabled = true;
        }

        startTime = Time.realtimeSinceStartup;
        image.CrossFadeAlpha(0.0f, 0.0f, true);
    }

    // Update is called once per frame
    void Update()
    {
        nowTime = Time.realtimeSinceStartup - startTime;

        if ((int)nowTime == (int)startFade)
        {
            image.CrossFadeAlpha(1.0f, fadeTime, true);
        }

        if ((int)nowTime == (int)endFade)
        {
            image.CrossFadeAlpha(0.0f, (fadeTime / 2), true);
        }
    }
}
