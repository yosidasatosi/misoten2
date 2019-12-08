using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamage : DamageBase
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerBase>();
        if (player)
        {
            SendDamage(player);
        }
    }
}
