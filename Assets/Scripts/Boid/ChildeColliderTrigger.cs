using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildeColliderTrigger : MonoBehaviour
{
    Boid boidCollider;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ColliderTriggerParent = gameObject.transform.parent.gameObject;
        boidCollider = ColliderTriggerParent.GetComponent<Boid>();
    }

    private void OnTriggerEnter(Collider other)
    {
        boidCollider.RelayOnTriggerEnter(other);
    }
}
