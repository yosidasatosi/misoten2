using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public int InitHP = 100;
    public UIHP HPUI;

    public  int HP { get; private set; }
    public int PlayerNo { get; private set; }

    private DamageFlash MyDamageFlash;

    // Start is called before the first frame update
    void Start()
    {
        HP = InitHP;
        MyDamageFlash = GetComponent<DamageFlash>();
        PlayerNo = GetComponent<PlayerController>()?.PlayerNo ?? 1;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Damage(int damagePoint)
    {
        if(MyDamageFlash && MyDamageFlash.enabled)
        {
            return;
        }

        HP = Mathf.Max(HP - damagePoint, 0);
        if(MyDamageFlash)
        {
            MyDamageFlash.enabled = true;
        }
        if(HPUI)
        {
            HPUI.SetPercent(HP / (float)InitHP);
        }
    }

}
