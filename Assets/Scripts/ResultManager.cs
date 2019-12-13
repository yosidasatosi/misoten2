using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public GameObject player;
    private int HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = player.GetComponent<PlayerBase>().HP;

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
