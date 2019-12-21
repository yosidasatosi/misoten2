using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AnimaroidLib;

[RequireComponent(typeof(RawImage))]
public class FadeInOut : Singleton<FadeInOut> {

    public const float fadeTime = 1.0f;

    public enum EState
    {
        None,
        FadeIn,
        FadeOut,
    }

    public EState State { get; private set; }
    private RawImage image;
    private System.Action callBack;
    private Timer timer;

    public float Alpha
    {
        get
        {
            return image.color.a;
        }
        set
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, value);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        var canvas = GameObject.Find("Canvas");
        if(canvas == null)
        {
            var com = gameObject.AddComponent<Canvas>();
            com.renderMode = RenderMode.ScreenSpaceOverlay;
            com.sortingOrder = 100;
        }
        else
        {
            transform.SetParent(canvas.transform);
        }


        image = GetComponent<RawImage>();
        image.color = Color.black;

        var rectTransform = image.rectTransform;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;

        State = EState.None;

        timer = new Timer(fadeTime);

        enabled = false;
    }

    private void FixedUpdate()
    {
        timer++;
        Alpha = Mathf.Clamp01(State == EState.FadeIn ? timer.InverseProgress : timer.Progress);

        if(timer.TimesUp())
        {
            State = EState.None;
            enabled = false;
            callBack?.Invoke();
        }
    }

    public void FadeIn(float fadeTime, System.Action callBack = null)
    {
        State = EState.FadeIn;
        this.callBack = callBack;
        timer.Reset(fadeTime);
        enabled = true;
    }

    public void FadeIn(System.Action callBack = null)
    {
        FadeIn(fadeTime, callBack);
    }

    public void FadeOut(float fadeTime, System.Action callBack = null)
    {
        State = EState.FadeOut;
        this.callBack = callBack;
        timer.Reset(fadeTime);
        enabled = true;
    }

    public void FadeOut(System.Action callBack = null)
    {
        FadeOut(fadeTime, callBack);
    }


}
