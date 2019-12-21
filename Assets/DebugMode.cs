using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMode : SingletonMonoBehaviour<DebugMode>
{
    private KeyCode[] KeyMap = new KeyCode[]
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0,
    };

    [RuntimeInitializeOnLoadMethod()]
    static void Init()
    {
        if(!Debug.isDebugBuild)
        {
            return;
        }
        if(GameObject.Find("DebugMode") == null)
        {
            var obj = new GameObject("DebugMode");
            obj.AddComponent<DebugMode>();
        }
    }

    protected override void Awake()
    {
        dontDestroyOnLoad = true;
        base.Awake();
    }

    private void OnEnable()
    {
        if(!Debug.isDebugBuild)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < KeyMap.Length; i++)
        {
            if (Input.GetKeyDown(KeyMap[i]))
            {
                LoadScene(i);
                break;
            }
        }
    }

    public void LoadScene(int sceneIndex)
    {
        FadeInOut.Instance.FadeOut(1.0f, () => SceneManager.LoadScene(sceneIndex));
    }
}
