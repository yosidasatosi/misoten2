using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogoEffect : MonoBehaviour
{
    SpriteRenderer logoSprite;

    [SerializeField]
    private ParticleSystem particle;

    //bubble
    private GameObject bubble;
    //camera
    private GameObject camera;


    
    // Start is called before the first frame update
    void Start()
    {
        logoSprite = GetComponent<SpriteRenderer>();
        bubble = GameObject.Find("Bubble");
        camera = GameObject.Find("Camera");
        particle.Stop();
        bubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameStart();
    }


    void GameStart()
    {
        Vector3 cameraPos = camera.transform.position;

        if (cameraPos.z >= -5.3f && Input.GetKeyDown(KeyCode.Space))
        {
            logoSprite.enabled = false;    
            particle.Play();
        }

        if (cameraPos.z >= -20f)
        {
            bubble.SetActive(true);
        }

    }




}
