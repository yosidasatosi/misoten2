using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    private int HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = GetComponent<PlayerBase>().HP;

        if(HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
