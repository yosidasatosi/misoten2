using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHP : MonoBehaviour
{
    
    private Slider MySlider;
    private void Start()
    {
        

        MySlider = GetComponent<Slider>();
    }

    public void SetPercent(float value)
    {
        if(MySlider)
        {
            MySlider.SetValueWithoutNotify(Mathf.Clamp01(value));
        }
    }
}
