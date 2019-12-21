using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : SceneControllerBase
{
    public Animation StageNameAnim;
    public float TransitionTime = 40;
    float TransitionTimer = 0;
    bool IsEndding = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        OnFadeInFinished = () => { if (StageNameAnim) StageNameAnim.Play(); };
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        TransitionTimer += Time.deltaTime;

        if (!IsEndding && TransitionTimer >= TransitionTime)
        {
            IsEndding = true;
            GoNextScene();
        }
    }
}
