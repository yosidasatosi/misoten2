using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FadeInOut.Instance.FadeIn(1.0f);
        LedState.Instance.Set(LedState.Situation.RESULT);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("push1") || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
