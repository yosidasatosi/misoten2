using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public float FlashSpeed = 1.0f;
    public float HideThreshold = 0.5f;
    public float TimeBeforeDisable = 1.0f;
    public bool EnableFlash { get; set; } = false;

    private float Timer;
    private float DisableTimer;
    private bool LastShowed;
    private bool ShouldDisable;
    private Renderer[] RendererList;

    // Start is called before the first frame update
    void Start()
    {
        RendererList = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!EnableFlash)
        {
            return;
        }

        Timer += Time.deltaTime;
        float opacity = Mathf.Abs(Mathf.Sin(Timer * FlashSpeed));
        bool show = opacity > HideThreshold;

        if (LastShowed != show)
        {
            EnableRenderers(show);
            LastShowed = show;
        }

        if(ShouldDisable)
        {
            DisableTimer += Time.deltaTime;
            if(DisableTimer >= TimeBeforeDisable)
            {
                EnableFlash = false;
                EnableRenderers(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnableFlash = true;
            LastShowed = true;
            ShouldDisable = false;
            Timer = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            ShouldDisable = true;
            DisableTimer = 0.0f;            
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
