using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleLogoEffect : MonoBehaviour
{
    SpriteRenderer logoSprite;

    //cameraScript
    public  CameraRotation cameraRot;
    private bool CameraRot;

    [SerializeField]
    private ParticleSystem particle;

    //bubble
    private GameObject bubble;

    /// <summary>
    /// titleAnimation終了時にtrue
    /// </summary>
    public bool cameraAnimStop = false;


    
    // Start is called before the first frame update
    void Start()
    {
        CameraRot = cameraRot.GetRotStart();
        logoSprite = GetComponent<SpriteRenderer>();
        bubble = GameObject.Find("Bubble");
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

        if (cameraAnimStop && Input.GetKeyDown(KeyCode.Space))
        {
            logoSprite.enabled = false;
            particle.Play();
            //SceneManager.LoadScene("asase");
        }

        if (cameraAnimStop)
        {
            //CameraRot = true;
            bubble.SetActive(true);
        }

    }






}
