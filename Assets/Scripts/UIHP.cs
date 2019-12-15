using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHP : MonoBehaviour
{
    
    private Image MySlider;
    private void Start()
    {
        

        MySlider = GetComponent<Image>();
    }

    public void SetPercent(float value)
    {
        if(MySlider)
        {
            MySlider.fillAmount = Mathf.Clamp01(value);
        }
    }
}
