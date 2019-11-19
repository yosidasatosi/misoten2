using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    public string NextSceneName;
    public Animation StageNameAnim;
    public float TransitionTime = 40;
    float TransitionTimer = 0;
    // Start is called before the first frame update
    void Start()
    {

        FadeInOut.Instance.FadeIn(1.0f,() => StageNameAnim?.Play());
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
