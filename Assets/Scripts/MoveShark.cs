using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShark : MonoBehaviour
{
    [System.Serializable]
    public class InitData
    {
        public Vector3 position;
        public Vector3 rotation;
    }

    public List<InitData> InitDatas = new List<InitData>();
    public int MoveMode = 0;
    public float Speed =10.0f;

    private void OnEnable()
    {
        if(MoveMode < InitDatas.Count)
        {
            transform.localPosition = InitDatas[MoveMode].position;
            transform.localRotation = Quaternion.Euler(InitDatas[MoveMode].rotation);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, 0.0f, Speed * Time.deltaTime, Space.Self);

    }
}
