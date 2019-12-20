using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishAnimEventReceiver : MonoBehaviour
{
    MoveJellyfish MoveJellyfish;
    void Start()
    {
        MoveJellyfish = transform.parent.GetComponent<MoveJellyfish>();
    }
    void OnMoveUp()
    {
        if(MoveJellyfish)
        {
            MoveJellyfish.OnMoveUp();
        }
    }

    void OnStopMoveUp()
    {
        if(MoveJellyfish)
        {
            MoveJellyfish.OnStopMoveUp();
        }
    }
}
