using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public float FlashSpeed = 1.0f;
    public float HideThreshold = 0.5f;
    public float TimeBeforeDisable = 1.0f;

    private float Timer;
    private bool LastShowed;
    private Renderer[] RendererList;

    // Start is called before the first frame update
    void Start()
    {
        RendererList = GetComponentsInChildren<Renderer>();
    }

    private void OnEnable()
    {
        LastShowed = true;
        Timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        float opacity = Mathf.Abs(Mathf.Sin(Timer * FlashSpeed));
        bool show = opacity > HideThreshold;

        if (LastShowed != show)
        {
            EnableRenderers(show);
            LastShowed = show;
        }

        if (Timer >= TimeBeforeDisable)
        {
            EnableRenderers(true);
            enabled = false;
        }
    }

    private void EnableRenderers(bool InEnable)
    {
        foreach (var renderer in RendererList)
        {
            renderer.enabled = InEnable;
        }
    }

}
