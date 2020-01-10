using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnUIController : MonoBehaviour
{
    public GameObject SafePrefab;
    public GameObject DangerPrefab;
    private GameObject[] obj;
    private Color enemyColor;
    private Color animalColor;

    // Start is called before the first frame update
    void Start()
    {
        obj = new GameObject[10];
        enemyColor = new Color(1.0f, 0.30f, 0.30f);
        animalColor = new Color(0.30f, 1.0f, 0.35f);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            if (obj[i] == null) continue;
            Vector3 at = Camera.main.transform.position;
            at.y = obj[i].transform.position.y;
            // ビルボード処理
            obj[i].transform.LookAt(at);

            // 消滅処理
            if (Camera.main.transform.position.z > obj[i].transform.position.z)
            {
                Destroy(obj[i]);
            }
        }
    }

    public void SetLockOn(GameObject hitObj, GameObject prefab)
    {
        for (int i = 0; i < 10; i++)
        {
            // 格納チェック
            if (obj[i] != null) continue;

            // 動的生成
            obj[i] = Instantiate(prefab, hitObj.transform.position, Quaternion.identity);
            obj[i].transform.parent = hitObj.transform;
            obj[i].transform.position -= new Vector3(0.0f, 0.0f, 1.0f);
            obj[i].transform.rotation = Quaternion.identity;
            if(Camera.main.transform.position.z + 10.0f > obj[i].transform.position.z)
            {
                float sclVal = (Camera.main.transform.position.z + 10.0f) - obj[i].transform.position.z;
                sclVal /= 10.0f;
                obj[i].transform.localScale = new Vector3(sclVal, sclVal, sclVal);
            }
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            SetLockOn(other.gameObject, DangerPrefab);
        }
        else if (other.tag == "Animal")
        {
            SetLockOn(other.gameObject, SafePrefab);
        }
    }
}
