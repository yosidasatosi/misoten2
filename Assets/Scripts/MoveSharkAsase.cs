using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSharkAsase : MonoBehaviour
{
    [System.Serializable]
    public class InitData
    {
        public Vector3 position;
        public Vector3 rotation;
        public float limit;
    }

    public int Mode = 0;

    public float PopTime = 1.0f;

    public List<InitData> InitDatas = new List<InitData>();
    public int MoveMode = 0;
    public float Speed = 10.0f;

    private float sum;

    private GameObject gameobject;

    private IwashiManager IwashiScr;

    private void OnEnable()
    {
        // ゲームオブジェクトの探索
        gameobject = GameObject.Find("Iwashi");

        // IwashiManagerのSetIwashiメソッドを使用
        IwashiScr = gameobject.GetComponent<IwashiManager>();

        if (MoveMode < InitDatas.Count)
        {
            transform.localPosition = InitDatas[MoveMode].position;
            transform.localRotation = Quaternion.Euler(InitDatas[MoveMode].rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IwashiScr.GetDelete())
        {
            if (Mode == 1)
            {
                sum = InitDatas[MoveMode].limit + transform.localPosition.x;

                if (sum > 0 && sum > InitDatas[MoveMode].limit * 2 || sum < 0 && sum < InitDatas[MoveMode].limit * 2)
                {
                    if (MoveMode < InitDatas.Count - 1)
                    {
                        MoveMode++;
                        //MoveMode %= InitDatas.Count;

                        transform.localPosition = InitDatas[MoveMode].position;
                        transform.localRotation = Quaternion.Euler(InitDatas[MoveMode].rotation);
                    }
                }
            }

            transform.Translate(0.0f, 0.0f, Speed * Time.deltaTime, Space.Self);
        }
    }
}
