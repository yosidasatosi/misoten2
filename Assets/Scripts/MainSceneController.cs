using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : SingletonMonoBehaviour<MainSceneController>
{
    public string NextSceneName;
    public Animation StageNameAnim;
    public float TransitionTime = 40;
    public LedState.Situation DefaultLedSituation;
    float TransitionTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (StageNameAnim != null)
        {
            FadeInOut.Instance.FadeIn(1.0f, () => StageNameAnim?.Play());
        }
        LedState.Instance.Set(DefaultLedSituation);
    }

    // Update is called once per frame
    void Update()
    {

        TransitionTimer += Time.deltaTime;

        if (TransitionTimer >= TransitionTime)
        {
            SceneManager.LoadScene(NextSceneName);
        }
    }
}
