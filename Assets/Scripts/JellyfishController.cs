using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishController : MonoBehaviour
{
    public GameObject ParticlePrefab;
    public float ParticleScale = 0.5f;
    public float TimeBeforeDisable = 2.0f;
    public float StopTime = 0.3f;


    private float Timer = 0;
    private bool ParticleStop = false;
    private bool LastVisible = false;
    private GameObject Particle;
    private Collider MyCollider;
    private Renderer MyRenderer;

    // Start is called before the first frame update
    void Start()
    {
        MyCollider = GetComponent<SphereCollider>();
        MyRenderer = GetComponentInChildren<Renderer>();
        LastVisible = MyRenderer?.isVisible ?? false;
    }

    // Update is called once per frame
    void Update()
    {

        if (MyRenderer.isVisible)
        {
            Timer += Time.deltaTime;

            if (Timer >= (ParticleStop ? StopTime : TimeBeforeDisable))
            {
                ParticleStop = !ParticleStop;
                if (ParticleStop)
                {
                    Destroy(Particle);
                    LedState.Instance.Set(SceneControllerBase.Instance.DefaultLedSituation);
                }
                else
                {
                    LedState.Instance.Set(LedState.Situation.FLASH);
                    Particle = Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
                    Particle.transform.localScale = Vector3.one * ParticleScale;
                }
                if (MyCollider != null)
                {
                    MyCollider.enabled = !ParticleStop;

                }
                Timer = 0.0f;
            }
        }
        if(LastVisible && !MyRenderer.isVisible)
        {
            LedState.Instance.Set(SceneControllerBase.Instance.DefaultLedSituation);
        }
        LastVisible = MyRenderer.isVisible;

        if (transform.localPosition.x <= -10.0f)
        {
            Destroy(gameObject);

        }
    }

}
