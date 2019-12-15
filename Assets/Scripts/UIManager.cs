using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    public GameObject AvoidanceUI { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        AvoidanceUI = transform.Find("AvoidanceUI")?.gameObject;
    }
}
