using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public int InitHP = 100;
    public UIHP HPUI;
    
    public  int HP
    {
        get
        {
            return PrivateHP;
        }
        private set
        {
            PrivateHP = value;
            if (HPUI)
            {
                HPUI.SetPercent(HP / (float)InitHP);
            }
        }
    }
    public int PlayerNo { get; private set; }

    private int PrivateHP;
    private DamageFlash MyDamageFlash;
    private PlayerController Controller;
    private static int[] HPsave = new int[4] { -1, -1, -1, -1 };


    // Start is called before the first frame update
    void Start()
    {
        MyDamageFlash = GetComponent<DamageFlash>();
        Controller = GetComponent<PlayerController>();
        PlayerNo = Controller ?.PlayerNo ?? 1;
        HP = (HPsave[PlayerNo - 1] > -1) ? HPsave[PlayerNo - 1] : InitHP;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
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
        HPsave[PlayerNo - 1] = HP;
        if (MyDamageFlash)
        {
            MyDamageFlash.enabled = true;
        }
        if (HP <= 0)
        {
            Death();
        }
        
    }
    static public void ResetHpSave()
    {
        HPsave = new int[4] { -1, -1, -1, -1 };
    }

    static public int GetHPSave(int InPlayerNo)
    {
        return HPsave[InPlayerNo-1];
    }

    public void Death()
    {
        if(Controller)
        {
            Controller.enabled = false;
            Destroy(gameObject, 3.0f);
        }
    }
}
