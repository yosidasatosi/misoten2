using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishController : MonoBehaviour
{
    public float TimeBeforeDisable = 5.0f;
    public float StopTime = 2.0f;


    private float DisableTimer = 0;
    private float StopDisableTimer = 0;
    private bool ParticleStop = true;


    private GameObject Particle;
    // Start is called before the first frame update
    void Start()
    {
        Particle = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (ParticleStop == true)
        {
            DisableTimer += Time.deltaTime;
            if (DisableTimer >= TimeBeforeDisable)
            {
                Particle.SetActive(false);
                ParticleStop = false;
            }
            
        }
        if (ParticleStop == false)
        {
            StopDisableTimer += Time.deltaTime;
            if (StopDisableTimer >= StopTime)
            {
                Particle.SetActive(true);
                ParticleStop = true;
            }
        }

    }
}
