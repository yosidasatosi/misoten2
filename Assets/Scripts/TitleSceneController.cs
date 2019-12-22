using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : SceneControllerBase
{
    public bool CameraAnimStop = false;

    private TitleLogoEffect MyTitleLogoEffect;
    private bool IsEndding = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PlayerBase.ResetHpSave();
        MyTitleLogoEffect = FindObjectOfType<TitleLogoEffect>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("push1") || Input.GetKeyDown(KeyCode.Return))
        {
            if(!IsEndding && CameraAnimStop)
            //if(!IsEndding && CameraAnimStop && Input.GetKeyDown(KeyCode.Return))
            {
                IsEndding = true;
                MyTitleLogoEffect.StartEffect();
                StartCoroutine("GoNextSceneAfterEffect");
            }
        }
    }

    IEnumerator GoNextSceneAfterEffect()
    {
        yield return new WaitForSeconds(3.0f);
        GoNextScene();
    }
}
