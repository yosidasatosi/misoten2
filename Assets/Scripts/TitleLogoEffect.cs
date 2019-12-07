using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogoEffect : MonoBehaviour
{
    SpriteRenderer logoSprite;

    [SerializeField]
    private ParticleSystem particle;



    
    // Start is called before the first frame update
    void Start()
    {
        logoSprite = GetComponent<SpriteRenderer>();
        particle.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        GameStart();
    }


    void GameStart()
    {
        Color logoColor = logoSprite.color;
        
        if (logoColor.a>=1.0f&&Input.GetKeyDown(KeyCode.Space))
        {
            logoSprite.enabled = false;
            particle.Play();
        }

    }

}
