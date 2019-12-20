using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLed : MonoBehaviour
{
    private void OnEnable()
    {
        LedState.Instance.Set(LedState.Situation.DANGER);
    }

    private void OnDisable()
    {
        LedState.Instance.Set(MainSceneController.Instance.DefaultLedSituation);
    }
}
