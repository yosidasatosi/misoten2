using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSwapCollider : MonoBehaviour
{

    public System.Action OnMainCameraHit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCamera")
        {
            OnMainCameraHit?.Invoke();
        }
    }
}
