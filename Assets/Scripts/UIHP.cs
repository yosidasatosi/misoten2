using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHP : MonoBehaviour
{
    public float DamageEffectDuration = 0.5f;
    public float LowHPRatio = 0.3f;
    public Color ColorBlue = Color.blue;
    public Color ColorRed = Color.red;
    public Sprite IconBlue;
    public Sprite IconRed;
    public Sprite DecoreBlue;
    public Sprite DecoreRed;

    private Image GaugeBase;
    private Image Icon;
    private Image Decore;
    private bool IsDamageEffect = false;
    private AnkouLightRandomMove RandomMove;

    private void Start()
    {       
        GaugeBase = GetComponent<Image>();
        Icon = transform.GetChild(2).GetComponent<Image>();
        Decore = transform.GetChild(1).GetComponent<Image>();
        RandomMove = GetComponent<AnkouLightRandomMove>();
    }

    public void SetPercent(float value, bool IsDamage = false)
    {
        value = Mathf.Clamp01(value);
        bool lowHP = value <= LowHPRatio;

        if(GaugeBase)
        {
            GaugeBase.fillAmount = value;
            GaugeBase.color = lowHP ? ColorRed : ColorBlue;
        }
        if(Icon)
        {
            Icon.sprite = lowHP ? IconRed : IconBlue;
        }
        if(Decore)
        {
            Decore.sprite = lowHP ? DecoreRed : DecoreBlue;
        }
    }

    public void OnDamage()
    {
        StopCoroutine("StartDamageEffect");
        StartCoroutine("StartDamageEffect");
    }

    IEnumerator StartDamageEffect()
    {
        if(RandomMove)
        {
            RandomMove.enabled = true;
        }
        yield return new WaitForSeconds(DamageEffectDuration);

        if (RandomMove)
        {
            RandomMove.enabled = false;
        }
    }
}
