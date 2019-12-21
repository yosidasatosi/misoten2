using System.Collections;
using UnityEngine.SceneManagement;


public class SceneControllerBase : SingletonMonoBehaviour<SceneControllerBase>
{
    public string NextSceneName;
    public LedState.Situation DefaultLedSituation;

    protected System.Action OnFadeInFinished;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        FadeInOut.Instance.FadeIn(1.0f, OnFadeInFinished);
        LedState.Instance.Set(DefaultLedSituation);
    }

    public void GoNextScene()
    {
        FadeInOut.Instance.FadeOut(1.0f, () => SceneManager.LoadScene(NextSceneName));
    }

}
