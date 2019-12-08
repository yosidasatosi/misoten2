using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBase : MonoBehaviour
{
    public int DamagePoint = 10;
    
    public void SendDamage(PlayerBase playerBase)
    {
        playerBase.Damage(DamagePoint);
    }
    
}
