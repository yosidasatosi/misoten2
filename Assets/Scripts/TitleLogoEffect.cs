using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogoEffect : MonoBehaviour
{
    SpriteRenderer logoSprite;

    [SerializeField]
    ParticleSystem particle;
    
    // Start is called before the first frame update
    void Start()
    {
        logoSprite = GetComponent<SpriteRenderer>();
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            logoSprite.enabled = false;
            particle.Play();
        }
    }
}
