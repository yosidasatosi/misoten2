using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBase : MonoBehaviour
{
    public int InitHP = 100;
    public UIHP HPUI;
    public Material GhostMat;
    public GameObject Sumida;
    
    public  int HP
    {
        get
        {
            return PrivateHP;
        }
        private set
        {
            PrivateHP = value;
            HPsave[PlayerNo - 1] = HP;
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
            SetGhostMat();
        }
    }

    public void Damage(int damagePoint)
    {
        if((MyDamageFlash && MyDamageFlash.enabled) || HP <= 0)
        {
            return;
        }

        HP = Mathf.Max(HP - damagePoint, 0);
        if(HPUI)
        {
            HPUI.OnDamage();
        }
        if (MyDamageFlash)
        {
            MyDamageFlash.enabled = true;
        }
        if (HP <= 0)
        {
            Death();
            int sum = 0;
            for (int i = 0; i < 4; i++)
            {
                sum += HPsave[i];
            }
            if (sum == 0)
            {
                FadeInOut.Instance.FadeOut(1.0f, () => SceneManager.LoadScene("GameOver"));
            }
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
        SetGhostMat();
        Instantiate(Sumida, transform);
    }

    public void SetGhostMat()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material = GhostMat;
        }
    }

}
