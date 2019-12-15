using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayer : MonoBehaviour
{
    [Range(1,4)]
    public int PlayerNo = 1;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerBase.GetHPSave(PlayerNo) <= 0)
        {
            Destroy(gameObject);
        }
    }
}
